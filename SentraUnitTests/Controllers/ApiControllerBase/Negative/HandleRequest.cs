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
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenMediatorReturnsNull()
    {
        // Arrange
        var mockRequest = new Mock<IRequest<OperationResult>>();
        var apiControllerBase = new Mock<ApiControllerBase>();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.Object.HandleRequest(mockRequest.Object));
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultIsFailure()
    {
        // Arrange
        var mockRequest = new Mock<IRequest<OperationResult>>();
        var operationResult = new OperationResult(false, "Error message");
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);
        var apiControllerBase = new Mock<ApiControllerBase>
        {
            CallBase = true
        };
        apiControllerBase.Object.Mediator = mockMediator.Object;

        // Act
        var result = await apiControllerBase.Object.HandleRequest(mockRequest.Object);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal("Error message", badRequestResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ReturnsOkObjectResult_WhenOperationResultIsSuccess()
    {
        // Arrange
        var mockRequest = new Mock<IRequest<OperationResult>>();
        var operationResult = new OperationResult(true, "Success message");
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);
        var apiControllerBase = new Mock<ApiControllerBase>
        {
            CallBase = true
        };
        apiControllerBase.Object.Mediator = mockMediator.Object;

        // Act
        var result = await apiControllerBase.Object.HandleRequest(mockRequest.Object);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal("Success message", okResult.Value);
    }
}