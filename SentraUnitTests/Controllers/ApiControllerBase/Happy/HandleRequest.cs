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

        var apiControllerBase = new Mock<ApiControllerBase>
        {
            CallBase = true,
            Object = new ApiControllerBase
            {
                Mediator = mockMediator.Object
            }
        };

        // Act
        var result = await apiControllerBase.Object.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

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

        var apiControllerBase = new Mock<ApiControllerBase>
        {
            CallBase = true,
            Object = new ApiControllerBase
            {
                Mediator = mockMediator.Object
            }
        };

        // Act
        var result = await apiControllerBase.Object.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal(operationResult.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenOperationResultIsNull()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var apiControllerBase = new Mock<ApiControllerBase>
        {
            CallBase = true,
            Object = new ApiControllerBase
            {
                Mediator = mockMediator.Object
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.Object.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }
}