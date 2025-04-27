using System;
using System.Threading.Tasks;
using FluentAssertions;
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

        var apiControllerBase = new ApiTestController(mediatorMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }

    [Fact]
    public async Task HandleRequest_ReturnsInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ThrowsAsync(new Exception("An error occurred"));

        var apiControllerBase = new ApiTestController(mediatorMock.Object);

        // Act & Assert
        var result = await apiControllerBase.HandleRequest(new Mock<IRequest<OperationResult>>().Object);
        result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(500);
    }

    private class ApiTestController : ApiControllerBase
    {
        public ApiTestController(IMediator mediator) : base(mediator)
        {
        }
    }
}