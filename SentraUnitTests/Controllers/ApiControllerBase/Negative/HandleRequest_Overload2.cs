using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ApiControllerBaseTests
{
    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenResultIsNull()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var apiControllerBase = new ApiControllerBase { Mediator = mediatorMock.Object };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }

    [Fact]
    public async Task HandleRequest_ReturnsBadRequestObjectResult_WhenOperationResultHasErrors()
    {
        // Arrange
        var operationResult = new OperationResult { Success = false, Errors = new List<string> { "Error 1", "Error 2" } };
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(operationResult);

        var apiControllerBase = new ApiControllerBase { Mediator = mediatorMock.Object };

        // Act
        var result = await apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult.Value);
        Assert.Equal(operationResult.Errors, badRequestResult.Value);
    }

    [Fact]
    public async Task HandleRequest_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var exception = new InvalidOperationException("Test exception");
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).Throws(exception);

        var apiControllerBase = new ApiControllerBase { Mediator = mediatorMock.Object };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }
}