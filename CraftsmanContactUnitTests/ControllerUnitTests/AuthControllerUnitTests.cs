using CraftsmanContact.Controllers;
using CraftsmanContact.DTOs.User;
using CraftsmanContact.Models;
using CraftsmanContact.Services.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CraftsmanContactUnitTests.ControllerUnitTests;

[TestFixture]
public class AuthControllerUnitTests
{
    private Mock<UserManager<AppUser>> _userManagerMock;
    private Mock<ILogger<AuthController>> _loggerMock;
    private Mock<ITokenService> _tokenServiceMock;
    private AuthController _controller;

    [SetUp]
    public void Setup()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _loggerMock = new Mock<ILogger<AuthController>>();
        _tokenServiceMock = new Mock<ITokenService>();
        _controller = new AuthController(_userManagerMock.Object, _loggerMock.Object, _tokenServiceMock.Object);
    }
   
    
    [Test]
    public async Task RegisterAsync_ReturnsOk_IfRequestIsValid()
    {
        //Arrange
        var requestDto = new RegisterUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "123456789",
            Password = "TestPassword!0"
        };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _tokenServiceMock.Setup(x => x.CreateToken(It.IsAny<AppUser>()))
            .Returns("MockToken");

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }
    
    
    [Test]
    public async Task RegisterAsync_ReturnsStatusCode418_IfModelStateIsInvalid()
    {
        //Arrange
        _controller.ModelState.AddModelError("Email", "Email is required");

        var requestDto = new RegisterUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            PhoneNumber = "123456789",
            Password = "TestPassword!0"
        };

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.TypeOf<ObjectResult>());
        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(418));
    }
    
    
    [Test]
    public async Task RegisterAsync_ReturnsInternalServerError_IfUserCreationFails()
    {
        // Arrange
        var requestDto = new RegisterUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "123456789",
            Password = "TestPassword!0"
        };
        
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to create user." }));

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.InstanceOf<List<IdentityError>>());
    }
}