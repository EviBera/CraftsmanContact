using System.Data;
using CraftsmanContact.Controllers;
using CraftsmanContact.DTOs.OfferedService;
using CraftsmanContact.Models;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CraftsmanContactUnitTests.ControllerUnitTests;

[TestFixture]
public class OfferedServiceControllerUnitTests
{
    private Mock<ILogger<OfferedServiceController>> _loggerMock;
    private Mock<IOfferedServiceRepository> _offeredServiceRepositoryMock;
    private OfferedServiceController _controller; 
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<OfferedServiceController>>();
        _offeredServiceRepositoryMock = new Mock<IOfferedServiceRepository>();
        _controller =
            new OfferedServiceController(_loggerMock.Object, _offeredServiceRepositoryMock.Object);
    }

    
    [Test]
    public async Task GetAllAsync_ReturnsOkResult_IfRepositoryProvidesData()
    {
        //Arrange
        var mockData = new List<OfferedServiceDto>
        {
            new OfferedServiceDto
            {
                OfferedServiceId = 1,
                OfferedServiceName = "Test Service 1"
            },
            new OfferedServiceDto
            {
                OfferedServiceId = 2,
                OfferedServiceName = "Test Service 2"
            }
            
        };
        _offeredServiceRepositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetAllAsync();
        
        //Assert
        Assert.IsNotNull(result);
        
        var actionResult = result.Result as OkObjectResult;
        Assert.IsNotNull(actionResult);
        Assert.That(actionResult.StatusCode, Is.EqualTo(200));

        var returnedData = actionResult.Value as List<OfferedServiceDto>;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.Count, Is.EqualTo(mockData.Count));
    }
    
    
    [Test]
    public async Task GetAllAsync_ReturnsNotFound_IfRepositoryCanNotProvideData()
    {
        // Arrange
        var exceptionMessage = "An error occurred";
        _offeredServiceRepositoryMock.Setup(repository => repository.GetAllAsync()).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var result = await _controller.GetAllAsync();
        
        //Assert
        Assert.IsNotNull(result);
        
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
    }

    
    [Test]
    public async Task GetByIdAsync_ReturnsOkResult_IfRepositoryProvidesData()
    {
        //Arrange
        var serviceId = 22;
        var mockData = new OfferedServiceDto
        {
            OfferedServiceId = 22,
            OfferedServiceName = "Test Service 22",

        };
        _offeredServiceRepositoryMock.Setup(repository => repository.GetByIdAsync(serviceId)).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetByIdAsync(serviceId);
        
        //Assert
        Assert.IsNotNull(result);
        
        var actionResult = result.Result as OkObjectResult;
        Assert.IsNotNull(actionResult);
        Assert.That(actionResult.StatusCode, Is.EqualTo(200));

        var returnedData = actionResult.Value as OfferedServiceDto;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.OfferedServiceId, Is.EqualTo(mockData.OfferedServiceId));
        Assert.That(returnedData.OfferedServiceName, Is.EqualTo(mockData.OfferedServiceName));
    }

    
    [Test]
    public async Task GetByIdAsync_ReturnsNotFound_IfOfferedServiceDoesNotExist()
    {
        //Arrange
        var nonExistentServiceId = 999;
        _offeredServiceRepositoryMock.Setup(repository => 
            repository.GetByIdAsync(nonExistentServiceId)).ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.GetByIdAsync(nonExistentServiceId);
        
        //Assert
        Assert.IsNotNull(result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
    }
    
    
    [Test]
    public async Task GetByIdAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        // Arrange
        var serviceId = 1;
        var unexpectedErrorMessage = "Unexpected error occurred.";
        _offeredServiceRepositoryMock.Setup(repository => repository.GetByIdAsync(serviceId))
            .ThrowsAsync(new Exception(unexpectedErrorMessage));

        // Act
        var result = await _controller.GetByIdAsync(serviceId);

        // Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Error getting offered service"));
    }


    [Test]
    public async Task RegisterNewOfferedServiceAsync_ReturnsOk_IfRepositoryProvidesData()
    {
        //Arrange
        var inputDto = new CreateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Service 100",
            OfferedServiceDescription = "New testcase"
        };
        var expectedData = new OfferedServiceDto
        {
            OfferedServiceId = 100,
            OfferedServiceName = "Test Service 100",
            OfferedServiceDescription = "New testcase"
        };
        
        _offeredServiceRepositoryMock.Setup(repository => 
                repository.RegisterAsync(It.IsAny<CreateRequestOfferedServiceDto>()))
            .ReturnsAsync(new OfferedService 
            {
                OfferedServiceId = 100,
                OfferedServiceName = "Test Service 100",
                OfferedServiceDescription = "New testcase"
            });
        
        // Act
        var result = await _controller.RegisterNewOfferedServiceAsync(inputDto);

        // Assert
        Assert.IsNotNull(result);
        var actionResult = result.Result as OkObjectResult;
        Assert.IsNotNull(actionResult);
        Assert.That(actionResult.StatusCode, Is.EqualTo(200));

        var returnedData = actionResult.Value as OfferedServiceDto;
        Assert.IsNotNull(returnedData);
        // Assert that returnedData matches expectedDto
        Assert.That(returnedData.OfferedServiceId, Is.EqualTo(expectedData.OfferedServiceId));
        Assert.That(returnedData.OfferedServiceName, Is.EqualTo(expectedData.OfferedServiceName));
        Assert.That(returnedData.OfferedServiceDescription, Is.EqualTo(expectedData.OfferedServiceDescription));
    }
    
    
    [Test]
    public async Task RegisterNewOfferedServiceAsync_ReturnsBadRequest_IfExceptionOccurs()
    {
        // Arrange
        var serviceDto = new CreateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Service",
            OfferedServiceDescription = "Test Description"
        };
        var exceptionMessage = "Validation failed for the service";
        _offeredServiceRepositoryMock.Setup(repository => 
                repository.RegisterAsync(It.IsAny<CreateRequestOfferedServiceDto>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        var result = await _controller.RegisterNewOfferedServiceAsync(serviceDto);

        // Assert
        Assert.IsNotNull(result);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(badRequestResult.Value.ToString().Contains(exceptionMessage));
    }

    
    [Test]
    public async Task UpdateOfferedServiceAsync_ReturnsOk_IfUpdateIsSuccessful()
    {
        // Arrange
        var serviceId = 1;
        var serviceDto = new UpdateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Service",
            OfferedServiceDescription = "Test Description"
        };
        var updatedService = new OfferedServiceDto
        {
            OfferedServiceId = 1,
            OfferedServiceName = "Test Service",
            OfferedServiceDescription = "Test Description"
        };

        _offeredServiceRepositoryMock.Setup(repository => repository.UpdateAsync(serviceId, serviceDto))
            .ReturnsAsync(updatedService);

        // Act
        var result = await _controller.UpdateOfferedServiceAsync(serviceId, serviceDto);

        // Assert
        Assert.IsNotNull(result);
        var actionResult = result as OkObjectResult;
        Assert.IsNotNull(actionResult);
        Assert.That(actionResult.StatusCode, Is.EqualTo(200));
        Assert.That(actionResult.Value, Is.EqualTo(updatedService));
    }
    
    
    [Test]
    public async Task UpdateOfferedServiceAsync_ReturnsBadRequest_IfOfferedServiceDoesNotExist()
    {
        // Arrange
        var serviceId = 100;
        var serviceDto = new UpdateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Service",
            OfferedServiceDescription = "Test Description"
        };

        _offeredServiceRepositoryMock.Setup(repository => repository.UpdateAsync(serviceId, serviceDto))
            .ThrowsAsync(new RowNotInTableException());

        // Act
        var result = await _controller.UpdateOfferedServiceAsync(serviceId, serviceDto);

        // Assert
        Assert.IsNotNull(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
    }

    
    [Test]
    public async Task UpdateOfferedServiceAsync_ReturnsInternalServerError_OnUnexpectedException()
    {
        // Arrange
        var serviceId = 100;
        var serviceDto = new UpdateRequestOfferedServiceDto
        {
            OfferedServiceName = "Test Service",
            OfferedServiceDescription = "Test Description"
        };

        _offeredServiceRepositoryMock.Setup(repository => repository.UpdateAsync(serviceId, serviceDto))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        var result = await _controller.UpdateOfferedServiceAsync(serviceId, serviceDto);

        // Assert
        Assert.IsNotNull(result);
        var statusCodeResult = result as ObjectResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
    }
    
    
    [Test]
    public async Task DeleteOfferedServiceAsync_ReturnsOk_IfDeletionIsSuccessful()
    {
        // Arrange
        int serviceId = 1;
        _offeredServiceRepositoryMock.Setup(repository => repository.DeleteAsync(serviceId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteOfferedServiceAsync(serviceId);

        // Assert
        Assert.IsNotNull(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));

        // Verify the DeleteAsync method was called on the repository
        _offeredServiceRepositoryMock.Verify(repository => repository.DeleteAsync(serviceId), Times.Once);
    }

    
    [Test]
    public async Task DeleteOfferedServiceAsync_ReturnsBadRequest_IfIdIsInvalid()
    {
        // Arrange
        int invalidServiceId = 999;
        _offeredServiceRepositoryMock.Setup(repository => repository.DeleteAsync(invalidServiceId))
            .ThrowsAsync(new RowNotInTableException());

        // Act
        var result = await _controller.DeleteOfferedServiceAsync(invalidServiceId);

        // Assert
        Assert.IsNotNull(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        Assert.That(badRequestResult.Value, Is.EqualTo("This service does not exist."));

        // Verify the DeleteAsync method was called on the repository
        _offeredServiceRepositoryMock.Verify(repository => repository.DeleteAsync(invalidServiceId), Times.Once);
    }
    
    
    [Test]
    public async Task DeleteOfferedServiceAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        // Arrange
        int serviceId = 1;
        _offeredServiceRepositoryMock.Setup(repository => repository.DeleteAsync(serviceId))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        var result = await _controller.DeleteOfferedServiceAsync(serviceId);

        // Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Unexpected error"));

        // Verify the DeleteAsync method was called on the repository
        _offeredServiceRepositoryMock.Verify(repository => repository.DeleteAsync(serviceId), Times.Once);
    }
}