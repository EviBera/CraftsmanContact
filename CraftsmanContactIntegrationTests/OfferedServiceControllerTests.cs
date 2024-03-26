using System.Net;
using System.Net.Http.Json;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs.OfferedService;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraftsmanContactIntegrationTests;

public class OfferedServiceControllerTests : IClassFixture<CraftsmanContactWebApplicationFactory>
{
    
    private readonly HttpClient _client;
    private readonly CraftsmanContactWebApplicationFactory _factory;

    public OfferedServiceControllerTests(CraftsmanContactWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    
    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithAllServices()
    {
        //Arrange - the seeded DB contains 2 entities
        int expectedCount = 2;
        
        // Act
        var response = await _client.GetAsync("/api/offeredservice/all");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedServices = await response.Content.ReadFromJsonAsync<List<OfferedServiceDto>>();
    
        // Assuming you know the exact count of services you've seeded.
        Assert.NotNull(returnedServices);
        Assert.Equal(expectedCount, returnedServices.Count);
        returnedServices[0].OfferedServiceName.Should().Contain("Update this");
        returnedServices[1].OfferedServiceName.Should().Contain("Delete this");

    }
    
    
    [Fact]
    public async Task GetByIdAsync_ReturnsOk_IfServiceIsFoundInDB()
    {
        //Arrange
        var serviceId = 1; // Assuming this is the ID you're using in your seeder

        // Act
        var response = await _client.GetAsync($"/api/offeredservice/{serviceId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var returnedService = await response.Content.ReadFromJsonAsync<OfferedServiceDto>();
        Assert.NotNull(returnedService);
    }

    
    [Fact]
    public async Task RegisterNewOfferedServiceAsync_AddsNewOfferedServiceToDb()
    {
        //Arrange
        CreateRequestOfferedServiceDto newService = new CreateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Name 1",
            OfferedServiceDescription = "Test Description 1"
        };
        var request = JsonContent.Create(newService);

        //Act
        var response = await _client.PostAsync("/api/offeredservice", request);

        //Assert
        response.EnsureSuccessStatusCode();
        var offeredServiceResponse = await response.Content.ReadFromJsonAsync<OfferedServiceDto>();
        Assert.NotNull(offeredServiceResponse);
        offeredServiceResponse?.OfferedServiceId.Should().BePositive();
        offeredServiceResponse?.OfferedServiceName.Should().Contain("Test Name 1");
        offeredServiceResponse?.OfferedServiceDescription.Should().Contain("Test Description 1");
    }
    
    
    [Fact]
    public async Task UpdateOfferedServiceAsync_ReturnsOk_IfServiceIsFoundInDB()
    {
        // Arrange
        var updateServiceDto = new UpdateRequestOfferedServiceDto
        {
            OfferedServiceName = "Updated name",
            OfferedServiceDescription = "Test description."
        };
        var content = JsonContent.Create(updateServiceDto);

        // Act
        var response = await _client.PatchAsync($"/api/offeredservice/1", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var updatedService = await response.Content.ReadFromJsonAsync<OfferedServiceDto>();
        Assert.NotNull(updatedService);
        updatedService.OfferedServiceName.Should().Contain("Updated name");
        updateServiceDto.OfferedServiceDescription.Should().Contain("Test description.");
    }
    
    
    [Fact]
    public async Task DeleteOfferedServiceAsync_ReturnsOkAndDeletesService_IfServiceExists()
    {
        // Arrange
        var serviceId = 2;
        
        // Verify the service exists before deletion
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CraftsmanContactContext>();
            var serviceExistsBeforeDelete = await db.OfferedServices.AnyAsync(s => s.OfferedServiceId == serviceId);
            Assert.True(serviceExistsBeforeDelete, "Service does not exist before deletion.");
        }
        
        // Act
        var response = await _client.DeleteAsync($"/api/offeredservice/{serviceId}");

        // Assert
        response.EnsureSuccessStatusCode();
        
        // Verify the service no longer exists in the database
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CraftsmanContactContext>();
            var serviceExistsAfterDelete = await db.OfferedServices.AnyAsync(s => s.OfferedServiceId == serviceId);
            Assert.False(serviceExistsAfterDelete, "Service exists after deletion.");
        }
    }
}