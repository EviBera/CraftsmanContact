using System.Data;
using CraftsmanContact.Controllers;
using CraftsmanContact.DTOs.Deal;
using CraftsmanContact.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CraftsmanContactUnitTests.ControllerUnitTests;

[TestFixture]
public class DealControllerUnitTests
{
    private Mock<ILogger<DealController>> _loggerMock;
    private Mock<IDealRepository> _dealRepositoryMock;
    private DealController _controller;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<DealController>>();
        _dealRepositoryMock = new Mock<IDealRepository>();
        _controller = new DealController(_loggerMock.Object, _dealRepositoryMock.Object);
    }


    [Test]
    public async Task GetDealsByUserAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var userId = "Test Id";
        var mockData = new List<DealDto>
        {
            new DealDto
            {
                ClientId = "Test Id",
                CraftsmanId = "Another Id",
                CreatedAt = new DateTime(2024, 3, 20),
                DealId = 1,
                IsAcceptedByCraftsman = false,
                IsClosedByClient = false,
                IsClosedByCraftsman = false,
                OfferedServiceId = 2
            },
            new DealDto
            {
                ClientId = "Another Id",
                CraftsmanId = "Test Id",
                CreatedAt = new DateTime(2024, 3, 20),
                DealId = 2,
                IsAcceptedByCraftsman = false,
                IsClosedByClient = false,
                IsClosedByCraftsman = false,
                OfferedServiceId = 3
            }
        };
        _dealRepositoryMock.Setup(repository => repository.GetDealsByUserAsync(userId)).ReturnsAsync(mockData);
        
        //Act
        var result = await _controller.GetDealsByUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        var returnedData = objectResult.Value as List<DealDto>;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.Count, Is.EqualTo(mockData.Count));
        
        // Verify that the GetDealsByUserAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealsByUserAsync(userId), Times.Once);
    }


    [Test]
    public async Task GetDealsByUserAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var userId = "Test Id";
        _dealRepositoryMock.Setup(repository => repository.GetDealsByUserAsync(userId))
            .ThrowsAsync(new RowNotInTableException());
        
        //Act
        var result = await _controller.GetDealsByUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that the GetDealsByUserAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealsByUserAsync(userId), Times.Once);
    }


    [Test]
    public async Task GetDealsByUserAsync_ReturnsNotFound_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var userId = "Test Id";
        var errorMessage = "Error.";
        _dealRepositoryMock.Setup(repository => repository.GetDealsByUserAsync(userId))
            .ThrowsAsync(new Exception(errorMessage));
        
        //Act
        var result = await _controller.GetDealsByUserAsync(userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(404));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Error."));
        
        // Verify that the GetDealsByUserAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealsByUserAsync(userId), Times.Once);
    }


    [Test]
    public async Task GetDealByIdAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var dealId = 1;
        var mockData = new DealDto
        {
            ClientId = "Test Id",
            CraftsmanId = "Another Id",
            CreatedAt = new DateTime(2024, 3, 20),
            DealId = 1,
            IsAcceptedByCraftsman = false,
            IsClosedByClient = false,
            IsClosedByCraftsman = false,
            OfferedServiceId = 2
        };
        _dealRepositoryMock.Setup(repository => repository.GetDealByIdAsync(dealId)).ReturnsAsync(mockData);

        //Act
        var result = await _controller.GetDealByIdAsync(dealId);

        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        var returnedData = objectResult.Value as DealDto;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.DealId, Is.EqualTo(mockData.DealId));
        Assert.That(returnedData.ClientId, Is.EqualTo(mockData.ClientId));
        Assert.That(returnedData.CraftsmanId, Is.EqualTo(mockData.CraftsmanId));
        Assert.That(returnedData.OfferedServiceId, Is.EqualTo(mockData.OfferedServiceId));
        Assert.That(returnedData.CreatedAt, Is.EqualTo(mockData.CreatedAt));
        Assert.That(returnedData.IsAcceptedByCraftsman, Is.EqualTo(mockData.IsAcceptedByCraftsman));
        Assert.That(returnedData.IsClosedByClient, Is.EqualTo(mockData.IsClosedByClient));
        Assert.That(returnedData.IsClosedByCraftsman, Is.EqualTo(mockData.IsClosedByCraftsman));
        
        // Verify that the GetDealByIdAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealByIdAsync(dealId), Times.Once);
    }


    [Test]
    public async Task GetDealByIdAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var dealId = 1;
        _dealRepositoryMock.Setup(repository => repository.GetDealByIdAsync(dealId))
            .ThrowsAsync(new RowNotInTableException());

        //Act
        var result = await _controller.GetDealByIdAsync(dealId);

        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        
        // Verify that the GetDealByIdAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealByIdAsync(dealId), Times.Once);
    }


    [Test]
    public async Task GetDealByIdAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var dealId = 1;
        _dealRepositoryMock.Setup(repository => repository.GetDealByIdAsync(dealId))
            .ThrowsAsync(new Exception());

        //Act
        var result = await _controller.GetDealByIdAsync(dealId);

        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that the GetDealByIdAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDealByIdAsync(dealId), Times.Once);
    }


    [Test]
    public async Task AddNewDealAsync_ReturnsCreated_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var dealDto = new CreateDealRequestDto
        {
            ClientId = "Test Id",
            CraftsmanId = "Another Id",
            OfferedServiceId = 3
        };
        _dealRepositoryMock.Setup(repository => repository.CreateDealAsync(dealDto)).Returns(Task.CompletedTask);
        
        //Act
        var result = await _controller.AddNewDealAsync(dealDto);
        
        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<CreatedResult>(result);
        
        // Verify that the CreateDealAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.CreateDealAsync(dealDto), Times.Once);
    }


    [Test]
    public async Task AddNewDealAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var dealDto = new CreateDealRequestDto
        {
            ClientId = "Test Id",
            CraftsmanId = "Another Id",
            OfferedServiceId = 3
        };
        var errorMessage = "Invalid input.";
        _dealRepositoryMock.Setup(repository => repository.CreateDealAsync(dealDto))
            .ThrowsAsync(new RowNotInTableException(errorMessage));
        
        //Act
        var result = await _controller.AddNewDealAsync(dealDto);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue((objectResult.Value.ToString().Contains("Invalid input.")));
        
        // Verify that the CreateDealAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.CreateDealAsync(dealDto), Times.Once);
    }


    [Test]
    public async Task AddNewDealAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var dealDto = new CreateDealRequestDto
        {
            ClientId = "Test Id",
            CraftsmanId = "Another Id",
            OfferedServiceId = 3
        };
        _dealRepositoryMock.Setup(repository => repository.CreateDealAsync(dealDto))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.AddNewDealAsync(dealDto);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that the CreateDealAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.CreateDealAsync(dealDto), Times.Once);
    }


    [Test]
    public async Task AcceptDealAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var craftsmanId = "Test Id";
        var dealId = 1;
        _dealRepositoryMock.Setup(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = _controller.AcceptDealAsync(craftsmanId, dealId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        
        // Verify that the SetDealToAcceptedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId), Times.Once);
    }


    [Test]
    public async Task AcceptDealAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var craftsmanId = "Test Id";
        var dealId = 1;
        var errorMessage = "Invalid input.";
        _dealRepositoryMock.Setup(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId))
            .ThrowsAsync(new RowNotInTableException(errorMessage));
        
        //Act
        var result = _controller.AcceptDealAsync(craftsmanId, dealId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Invalid input."));
        
        // Verify that the SetDealToAcceptedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId), Times.Once);
    }


    [Test]
    public async Task AcceptDealAsync_ReturnsBadRequest_IfRepositoryThrowsArgumentException()
    {
        //Arrange
        var craftsmanId = "Test Id";
        var dealId = 1;
        var errorMessage = "Incorrect input.";
        _dealRepositoryMock.Setup(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId))
            .ThrowsAsync(new ArgumentException(errorMessage));
        
        //Act
        var result = _controller.AcceptDealAsync(craftsmanId, dealId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Incorrect input."));
        
        // Verify that the SetDealToAcceptedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId), Times.Once);
    }
    
    
    [Test]
    public async Task AcceptDealAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var craftsmanId = "Test Id";
        var dealId = 1;
        _dealRepositoryMock.Setup(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = _controller.AcceptDealAsync(craftsmanId, dealId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        
        // Verify that the SetDealToAcceptedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealToAcceptedAsync(craftsmanId, dealId), Times.Once);
    }


    [Test]
    public async Task CloseDealAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var dealId = 1;
        var userId = "Test Id";
        _dealRepositoryMock.Setup(repository => repository.SetDealClosedAsync(dealId, userId))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _controller.CloseDealAsync(dealId, userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        
        // Verify that the SetDealClosedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealClosedAsync(dealId, userId), Times.Once);
    }

    
    [Test]
    public async Task CloseDealAsync_ReturnsBadRequest_IfRepositoryThrowsArgumentException()
    {
        //Arrange
        var dealId = 1;
        var userId = "Test Id";
        var errorMessage = "Test message.";
        _dealRepositoryMock.Setup(repository => repository.SetDealClosedAsync(dealId, userId))
            .ThrowsAsync(new ArgumentException(errorMessage));
        
        //Act
        var result = await _controller.CloseDealAsync(dealId, userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Test message."));
        
        // Verify that the SetDealClosedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealClosedAsync(dealId, userId), Times.Once);
    }

    
    [Test]
    public async Task CloseDealAsync_ReturnsBadRequest_IfRepositoryThrowsRowNotInTableException()
    {
        //Arrange
        var dealId = 1;
        var userId = "Test Id";
        var errorMessage = "Test message.";
        _dealRepositoryMock.Setup(repository => repository.SetDealClosedAsync(dealId, userId))
            .ThrowsAsync(new RowNotInTableException(errorMessage));
        
        //Act
        var result = await _controller.CloseDealAsync(dealId, userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(400));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Test message."));
        
        // Verify that the SetDealClosedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealClosedAsync(dealId, userId), Times.Once);
    }

    
    [Test]
    public async Task CloseDealAsync_ReturnsInternalServerError_IfUnexpectedExceptionOccurs()
    {
        //Arrange
        var dealId = 1;
        var userId = "Test Id";
        _dealRepositoryMock.Setup(repository => repository.SetDealClosedAsync(dealId, userId))
            .ThrowsAsync(new Exception());
        
        //Act
        var result = await _controller.CloseDealAsync(dealId, userId);
        
        //Assert
        Assert.IsNotNull(result);
        var objectResult = result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Something went wrong."));
        
        // Verify that the SetDealClosedAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.SetDealClosedAsync(dealId, userId), Times.Once);
    }

    
    [Test]
    public async Task GetDeadDealsAsync_ReturnsOk_IfRepositoryDoesNotThrowException()
    {
        //Arrange
        var mockData = new List<DealDto>
        {
            new DealDto
            {
                DealId = 1,
                ClientId = "Test Id 1",
                CraftsmanId = "Test Id 2",
                OfferedServiceId = 1,
                CreatedAt = new DateTime(2024, 3, 22),
                IsAcceptedByCraftsman = true,
                IsClosedByCraftsman = false,
                IsClosedByClient = false
            },
            new DealDto
            {
                DealId = 2,
                ClientId = "Test Id 3",
                CraftsmanId = "Test Id 4",
                OfferedServiceId = 1,
                CreatedAt = new DateTime(2024, 3, 22),
                IsAcceptedByCraftsman = false,
                IsClosedByCraftsman = false,
                IsClosedByClient = false
            },
            new DealDto
            {
                DealId = 3,
                ClientId = "Test Id 5",
                CraftsmanId = "Test Id 2",
                OfferedServiceId = 1,
                CreatedAt = new DateTime(2024, 3, 22),
                IsAcceptedByCraftsman = false,
                IsClosedByCraftsman = false,
                IsClosedByClient = false
            }
        };
        _dealRepositoryMock.Setup(repository => repository.GetDeadDealsAsync()).ReturnsAsync(mockData);

        //Act
        var result = await _controller.GetDeadDealsAsync();

        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        var returnedData = objectResult.Value as List<DealDto>;
        Assert.IsNotNull(returnedData);
        Assert.That(returnedData.Count, Is.EqualTo(mockData.Count));
        
        // Verify that the GetDeadDealsAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDeadDealsAsync(), Times.Once);
    }
    
    
    [Test]
    public async Task GetDeadDealsAsync_ReturnsInternalServerError_IfRepositoryThrowsException()
    {
        //Arrange
        var errorMessage = "Test message.";
        _dealRepositoryMock.Setup(repository => repository.GetDeadDealsAsync()).ThrowsAsync(new Exception(errorMessage));

        //Act
        var result = await _controller.GetDeadDealsAsync();

        //Assert
        Assert.IsNotNull(result);
        var objectResult = result.Result as ObjectResult;
        Assert.IsNotNull(objectResult);
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));
        Assert.IsTrue(objectResult.Value.ToString().Contains("Test message"));
        
        // Verify that the GetDeadDealsAsync method was called on the repository
        _dealRepositoryMock.Verify(repository => repository.GetDeadDealsAsync(), Times.Once);
    }
}