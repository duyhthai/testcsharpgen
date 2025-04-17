using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ApiControllerBaseTests
{
    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenOperationResultIsNull()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var apiControllerBase = new ApiTestController(mediatorMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(null));
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenOperationResultIsInvalid()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { IsValid = false });
        var apiControllerBase = new ApiTestController(mediatorMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(It.IsAny<IRequest<OperationResult>>()));
    }

    private class ApiTestController : ApiControllerBase
    {
        public ApiTestController(IMediator mediator) : base(mediator)
        {
        }

        public override Task<IActionResult> HandleRequest(IRequest<OperationResult> request)
        {
            return base.HandleRequest(request);
        }
    }
}