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
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { IsSuccess = true });

        var controller = new ApiControllerBase(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(It.IsAny<IRequest<OperationResult>>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultIsFailure()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { IsSuccess = false, Errors = new List<string> { "Error" } });

        var controller = new ApiControllerBase(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(It.IsAny<IRequest<OperationResult>>());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenResultIsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var controller = new ApiControllerBase(mockMediator.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(It.IsAny<IRequest<OperationResult>>()));
    }
}