using System;
using System.Threading.Tasks;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ApiControllerBaseTests
{
    [Fact]
    public async Task HandleRequest_ReturnsOkObjectResult_WhenOperationResultIsSuccess()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var operationResult = new OperationResult { Success = true };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);

        var apiController = new ApiControllerBase();

        // Act
        var result = await apiController.HandleRequest(new Mock<IRequest<OperationResult>>().Object, mockMediator.Object);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal(operationResult, okResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultIsFailure()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var operationResult = new OperationResult { Success = false, Errors = new List<string> { "Error" } };
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);

        var apiController = new ApiControllerBase();

        // Act
        var result = await apiController.HandleRequest(new Mock<IRequest<OperationResult>>().Object, mockMediator.Object);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal(operationResult.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenMediatorReturnsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var apiController = new ApiControllerBase();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            apiController.HandleRequest(new Mock<IRequest<OperationResult>>().Object, mockMediator.Object));
    }
}