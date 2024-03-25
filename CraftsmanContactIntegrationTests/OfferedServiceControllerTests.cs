using System.Net.Http.Json;
using CraftsmanContact.DTOs.OfferedService;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

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
}