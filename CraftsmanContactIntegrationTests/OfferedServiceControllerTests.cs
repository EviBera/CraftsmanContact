using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CraftsmanContact.Data;
using CraftsmanContact.DTOs.OfferedService;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CraftsmanContactIntegrationTests;

[Collection(("Integration tests"))]
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
    public async Task GetAllAsync_ReturnsOk_WithAllOfferedServices()
    {
        //Arrange
        _factory.ResetDatabase();
        //The seeded DB contains 3 entities
        int expectedCount = 3;
        
        // Act
        var response = await _client.GetAsync("/api/offeredservice/all");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedServices = await response.Content.ReadFromJsonAsync<List<OfferedServiceDto>>();
        
        Assert.NotNull(returnedServices);
        Assert.Equal(expectedCount, returnedServices.Count);
        returnedServices[0].OfferedServiceName.Should().Contain("Test service 1");
        returnedServices[1].OfferedServiceName.Should().Contain("Delete this");
        returnedServices[2].OfferedServiceName.Should().Contain("Update this");
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ReturnsOk_IfOfferedServiceExists()
    {
        //Arrange
        _factory.ResetDatabase();
        var serviceId = 11;

        // Act
        var response = await _client.GetAsync($"/api/offeredservice/{serviceId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var returnedService = await response.Content.ReadFromJsonAsync<OfferedServiceDto>();
        Assert.NotNull(returnedService);
        returnedService.OfferedServiceName.Should().Contain("Test service 1");
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_IfOfferedServiceDoesNotExist()
    {
        // Arrange
        _factory.ResetDatabase();
        var nonExistentServiceId = 999;

        // Act
        var response = await _client.GetAsync($"/api/offeredservice/{nonExistentServiceId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    
    [Fact]
    public async Task RegisterNewOfferedServiceAsync_AddsNewOfferedServiceToDb()
    {
        //Arrange
        _factory.ResetDatabase();
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
    public async Task RegisterNewOfferedServiceAsync_ReturnsBadRequest_IfDataIsInvalid()
    {
        // Arrange
        var invalidServiceDto = new CreateRequestOfferedServiceDto
        {
            OfferedServiceName = "",
            OfferedServiceDescription = "Name is missing, so the request is invalid."
        };
        _client.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var content = JsonContent.Create(invalidServiceDto);

        // Act
        var response = await _client.PostAsync("/api/offeredservice", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    
    [Fact]
    public async Task UpdateOfferedServiceAsync_ReturnsOk_IfOfferedServiceExists()
    {
        // Arrange
        _factory.ResetDatabase();
        var serviceId = 11;
        var updateServiceDto = new UpdateRequestOfferedServiceDto
        {
            OfferedServiceName = "Updated name",
            OfferedServiceDescription = "Test description."
        };
        var content = JsonContent.Create(updateServiceDto);

        // Act
        var response = await _client.PatchAsync($"/api/offeredservice/{serviceId}", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var updatedService = await response.Content.ReadFromJsonAsync<OfferedServiceDto>();
        Assert.NotNull(updatedService);
        updatedService.OfferedServiceName.Should().Contain("Updated name");
        updateServiceDto.OfferedServiceDescription.Should().Contain("Test description.");
    }
    
    
    [Fact]
    public async Task DeleteOfferedServiceAsync_ReturnsOkAndDeletesService_IfOfferedServiceExists()
    {
        // Arrange
        _factory.ResetDatabase();
        var serviceId = 12;
        
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
    
    
    [Fact]
    public async Task DeleteOfferedServiceAsync_ReturnsBadRequest_IfOfferedServiceDoesNotExist()
    {
        // Arrange
        _factory.ResetDatabase();
        var nonExistentServiceId = 999;

        // Act
        var response = await _client.DeleteAsync($"/api/offeredservice/{nonExistentServiceId}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}