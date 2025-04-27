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
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        var apiController = new ApiControllerBase();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiController.HandleRequest(new Mock<IRequest<OperationResult>>().Object, mockMediator.Object));
    }

    [Fact]
    public async Task HandleRequest_ThrowsInvalidOperationException_WhenMediatorIsNullAndNoTestMediatorProvided()
    {
        // Arrange
        var apiController = new ApiControllerBase();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => apiController.HandleRequest(new Mock<IRequest<OperationResult>>().Object));
    }
}