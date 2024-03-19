using System.Data;
using CraftsmanContact.Controllers;
using CraftsmanContact.DTOs.OfferedService;
using CraftsmanContact.DTOs.User;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CraftsmanContactUnitTests.ControllerUnitTests;

[TestFixture]
public class UserControllerUnitTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ILogger<UserController>> _loggerMock;
    private UserController _controller;
    
    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<UserController>>();
        _controller = new UserController(_userRepositoryMock.Object, _loggerMock.Object);
    }

    
    [Test]
    public async Task RegisterAsync_ReturnsOk_IfRepositoryProvidesSucceededResult()
    {
        var requestDto = new RegisterUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "123456789",
            Password = "TestPassword!0"
        };
        
        var successResult = IdentityResult.Success;

        _userRepositoryMock.Setup(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()))
            .ReturnsAsync(successResult);

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));

        // Verify that the RegisterUserAsync method was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()), Times.Once);
    }
    
    
    [Test]
    public async Task RegisterAsync_ReturnsBadRequest_IfRepositoryDoesNotProvidesSucceededResult()
    {
        var requestDto = new RegisterUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "123456789",
            Password = "TestPassword!0"
        };
        
        var errors = new[] { new IdentityError { Code = "TestError", Description = "Test error description" } };
        var failureResult = IdentityResult.Failed(errors);
        
        _userRepositoryMock.Setup(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()))
            .ReturnsAsync(failureResult);

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));

        // Ensure the result contains the errors from the IdentityResult
        var resultValue = badRequestResult.Value as IdentityResult;
        Assert.IsNotNull(resultValue);
        Assert.IsFalse(resultValue.Succeeded);
        Assert.Contains(errors[0], resultValue.Errors.ToList());
        
        // Verify that the RegisterUserAsync method was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()), Times.Once);
    }
    
    
    [Test]
    public async Task RegisterAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
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
    
        // Set up the mock to throw an exception when RegisterUserAsync is called
        _userRepositoryMock.Setup(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        var result = await _controller.RegisterAsync(requestDto);

        // Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Unexpected error"));

        // Verify that RegisterUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterUserAsync(It.IsAny<RegisterUserRequestDto>()), Times.Once);
    }


    [Test]
    public async Task DeleteUserAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var userId = "test Id";
        _userRepositoryMock.Setup(repository => repository.DeleteUserAsync(userId)).Returns(Task.CompletedTask);
        
        //Act
        var result = await _controller.DeleteUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        
        // Verify that DeleteUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.DeleteUserAsync(userId), Times.Once);
    }
    
    
    [Test]
    public async Task DeleteUserAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "test Id";
        _userRepositoryMock.Setup(repository => repository.DeleteUserAsync(userId))
            .ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.DeleteUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        
        // Verify that DeleteUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.DeleteUserAsync(userId), Times.Once);
    }
    
    
    [Test]
    public async Task DeleteUserAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "test Id";
        _userRepositoryMock.Setup(repository => repository.DeleteUserAsync(userId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.DeleteUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that DeleteUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.DeleteUserAsync(userId), Times.Once);
    }


    [Test]
    public async Task UpdateUserAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var userId = "Test id";
        var updateDto = new UpdateUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "12345678"
        };
        _userRepositoryMock.Setup(repository => repository.UpdateUserAsync(userId, updateDto))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _controller.UpdateUserAsync(userId, updateDto);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        
        // Verify that UpdateUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.UpdateUserAsync(userId, updateDto), Times.Once);
    }

    
    [Test]
    public async Task UpdateUserAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "Test id";
        var updateDto = new UpdateUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "12345678"
        };
        _userRepositoryMock.Setup(repository => repository.UpdateUserAsync(userId, updateDto))
            .ThrowsAsync(new RowNotInTableException());

        //Act
        var result = await _controller.UpdateUserAsync(userId, updateDto);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that UpdateUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.UpdateUserAsync(userId, updateDto), Times.Once);
    }
    
    
    [Test]
    public async Task UpdateUserAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "Test id";
        var updateDto = new UpdateUserRequestDto
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com",
            PhoneNumber = "12345678"
        };
        _userRepositoryMock.Setup(repository => repository.UpdateUserAsync(userId, updateDto))
            .ThrowsAsync(new Exception());

        //Act
        var result = await _controller.UpdateUserAsync(userId, updateDto);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that UpdateUserAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.UpdateUserAsync(userId, updateDto), Times.Once);
    }


    [Test]
    public async Task GetUserByIdAsync_ReturnsOk_IfRepositoryProvidesData()
    {
        //Arrange
        var userId = "Test Id";
        var mockData = new UserDto
        {
            Id = "Test Id",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@email.com"
        };
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId)).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetUserByIdAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));

        var returnedUser = objectResult.Value as UserDto;
        Assert.IsNotNull(returnedUser); 
        Assert.That(returnedUser.Id, Is.EqualTo(mockData.Id));
        Assert.That(returnedUser.FirstName, Is.EqualTo(mockData.FirstName));
        Assert.That(returnedUser.LastName, Is.EqualTo(mockData.LastName));
        Assert.That(returnedUser.Email, Is.EqualTo(mockData.Email));
        
        // Verify that GetUserByIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetUserByIdAsync(userId), Times.Once);
    }

    
    [Test]
    public async Task GetUserByIdAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "Test Id";
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId))
            .ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.GetUserByIdAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that GetUserByIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetUserByIdAsync(userId), Times.Once);
    }
    
    
    [Test]
    public async Task GetUserByIdAsync_ReturnsBadRequest_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "Test Id";
        _userRepositoryMock.Setup(repository => repository.GetUserByIdAsync(userId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.GetUserByIdAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that GetUserByIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetUserByIdAsync(userId), Times.Once);
    }


    [Test]
    public async Task RegisterServiceAsOfferedByUserAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        _userRepositoryMock.Setup(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _controller.RegisterServiceAsOfferedByUserAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        
        // Verify that RegisterServiceForCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId), Times.Once);
    }

    
    [Test]
    public async Task RegisterServiceAsOfferedByUserAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        var errorMessage = "This user does not exist.";
        _userRepositoryMock.Setup(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId))
            .ThrowsAsync(new RowNotInTableException(errorMessage));
        
        //Act
        var result = await _controller.RegisterServiceAsOfferedByUserAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("This user does not exist."));
        
        // Verify that RegisterServiceForCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId), Times.Once);
    }
    
    
    [Test]
    public async Task RegisterServiceAsOfferedByUserAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        _userRepositoryMock.Setup(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.RegisterServiceAsOfferedByUserAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that RegisterServiceForCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RegisterServiceForCraftsmanAsync(userId, serviceId), Times.Once);
    }


    [Test]
    public async Task RemoveOfferedServiceFromCraftsmanAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        _userRepositoryMock.Setup(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _controller.RemoveOfferedServiceFromCraftsmanAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        
        // Verify that RemoveServiceOfCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId), Times.Once);
    }

    
    [Test]
    public async Task RemoveOfferedServiceFromCraftsmanAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        var errorMessasge = "Invalid input.";
        _userRepositoryMock.Setup(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId))
            .ThrowsAsync(new RowNotInTableException(errorMessasge));
        
        //Act
        var result = await _controller.RemoveOfferedServiceFromCraftsmanAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Invalid input."));
        
        // Verify that RemoveServiceOfCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId), Times.Once);
    }

    
    [Test]
    public async Task RemoveOfferedServiceFromCraftsmanAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "Test Id";
        var serviceId = 1;
        var errorMessasge = "Invalid input.";
        _userRepositoryMock.Setup(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId))
            .ThrowsAsync(new Exception(errorMessasge));
        
        //Act
        var result = await _controller.RemoveOfferedServiceFromCraftsmanAsync(userId, serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Invalid input."));
        
        // Verify that RemoveServiceOfCraftsmanAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.RemoveServiceOfCraftsmanAsync(userId, serviceId), Times.Once);
    }

    
    [Test]
    public async Task GetCraftsmenByServiceAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var serviceId = 1;
        var mockData = new List<UserDto>
        {
            new UserDto
            {
                Id = "Test Id 1",
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Email = "test1@email.com"
            },
            new UserDto
            {
                Id = "Test Id 2",
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                Email = "test2@email.com"
            }
        };
        _userRepositoryMock.Setup(repository => repository.GetCraftsmenByServiceIdAsync(serviceId)).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetCraftsmenByServiceAsync(serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));

        var returnedData = objectResult.Value as List<UserDto>;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.Count, Is.EqualTo(mockData.Count));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetCraftsmenByServiceIdAsync(serviceId), Times.Once);
    }

    
    [Test]
    public async Task GetCraftsmenByServiceAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var serviceId = 1;
        _userRepositoryMock.Setup(repository => repository.GetCraftsmenByServiceIdAsync(serviceId))
            .ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.GetCraftsmenByServiceAsync(serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetCraftsmenByServiceIdAsync(serviceId), Times.Once);
    }

    
    [Test]
    public async Task GetCraftsmenByServiceAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var serviceId = 1;
        _userRepositoryMock.Setup(repository => repository.GetCraftsmenByServiceIdAsync(serviceId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.GetCraftsmenByServiceAsync(serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetCraftsmenByServiceIdAsync(serviceId), Times.Once);
    }

    
    [Test]
    public async Task GetServicesOfUserAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Assert
        var userId = "Test Id";
        var mockData = new List<OfferedServiceDto>
        {
            new OfferedServiceDto
            {
                OfferedServiceId = 1,
                OfferedServiceName = "Test Name 1",
                OfferedServiceDescription = "Test Description 1"
            },
            new OfferedServiceDto
            {
                OfferedServiceId = 2,
                OfferedServiceName = "Test Name 2",
                OfferedServiceDescription = "Test Description 2"
            },
        };
        _userRepositoryMock.Setup(repository => repository.GetServicesOfUserAsync(userId)).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetServicesOfUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));

        var returnedData = objectResult.Value as List<OfferedServiceDto>;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.Count, Is.EqualTo(mockData.Count));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetServicesOfUserAsync(userId), Times.Once);
    }

    
    [Test]
    public async Task GetServicesOfUserAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Assert
        var userId = "Test Id";
        _userRepositoryMock.Setup(repository => repository.GetServicesOfUserAsync(userId))
            .ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.GetServicesOfUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetServicesOfUserAsync(userId), Times.Once);
    }

    
    [Test]
    public async Task GetServicesOfUserAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Assert
        var userId = "Test Id";
        _userRepositoryMock.Setup(repository => repository.GetServicesOfUserAsync(userId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.GetServicesOfUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that GetCraftsmenByServiceIdAsync was called on the repository
        _userRepositoryMock.Verify(repository => repository.GetServicesOfUserAsync(userId), Times.Once);
    }
}