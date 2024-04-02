using System.Net;
using System.Net.Http.Json;
using CraftsmanContact.DTOs.User;
using FluentAssertions;

namespace CraftsmanContactIntegrationTests;

public class UserControllerTests : IClassFixture<CraftsmanContactWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CraftsmanContactWebApplicationFactory _factory;

    public UserControllerTests(CraftsmanContactWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    
    [Fact]
    public async Task GetUserByIdAsync_ReturnsOk_IfUserExists()
    {
        //Arrange
        _factory.ResetDatabase();
        var userId = "user2";

        //Act
        var result = await _client.GetAsync($"api/user/{userId}");

        //Assert
        result.EnsureSuccessStatusCode();
        var content = await result.Content.ReadFromJsonAsync<UserDto>();
        Assert.NotNull(content);
        content.FirstName.Should().Contain("Testuser2");
        content.LastName.Should().Contain("Two");
        content.Email.Should().Contain("test2@email.com");
    }


    [Fact]
    public async Task GetCraftsmenByServiceAsync_ReturnsProperCraftsmen_IfDBContainsCraftsmenWithSearchedService()
    {
        //Arrange
        _factory.ResetDatabase();
        var serviceId = 1;

        //Act
        var result = await _client.GetAsync($"api/user/craftsmenbyservice/{serviceId}");

        //Assert
        result.EnsureSuccessStatusCode();
        var content = await result.Content.ReadFromJsonAsync<List<UserDto>>();
        Assert.NotNull(content);
        Assert.Equal(2, content.Count());
        content[0].OfferedServices.Should().Contain(dto => dto.OfferedServiceId == serviceId);
        content[1].OfferedServices.Should().Contain(dto => dto.OfferedServiceId == serviceId);

    }

    
    [Fact]
    public async Task GetCraftsmenByServiceAsync_ReturnsProperCraftsman_IfDBContainsCraftsmanWithSearchedService()
    {
        //Arrange
        _factory.ResetDatabase();
        var serviceId = 2;

        //Act
        var result = await _client.GetAsync($"api/user/craftsmenbyservice/{serviceId}");

        //Assert
        result.EnsureSuccessStatusCode();
        var content = await result.Content.ReadFromJsonAsync<List<UserDto>>();
        Assert.NotNull(content);
        Assert.Single(content);
        content[0].OfferedServices.Should().Contain(dto => dto.OfferedServiceId == serviceId);
    }
    
    [Fact]
    public async Task GetCraftsmenByServiceAsync_ReturnsBadRequest_IfDBDoesNotContainService()
    {
        //Arrange
        _factory.ResetDatabase();
        var serviceId = 3;

        //Act
        var result = await _client.GetAsync($"api/user/craftsmenbyservice/{serviceId}");

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}