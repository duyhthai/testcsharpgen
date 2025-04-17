using HelloBear.Application.Auth.Commands.ForgotPassword;
using HelloBear.Application.Auth.Commands.Login;
using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public async Task HandleRequest_WithNullRequest_ThrowsInvalidOperationException()
    {
        // Arrange
        var controller = new AuthController();
        var mockMediator = new Mock<IMediator>();
        var request = (IRequest<OperationResult>)null;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(request, mockMediator.Object));
    }

    [Fact]
    public async Task HandleRequest_WithInvalidRequestType_ThrowsInvalidOperationException()
    {
        // Arrange
        var controller = new AuthController();
        var mockMediator = new Mock<IMediator>();
        var request = new object();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(request, mockMediator.Object));
    }

    [Fact]
    public async Task HandleRequest_WithNonNullableRequestAndNullResult_ThrowsInvalidOperationException()
    {
        // Arrange
        var controller = new AuthController();
        var mockMediator = new Mock<IMediator>();
        var request = new ForgotPasswordCommand();

        mockMediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync((OperationResult)null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(request, mockMediator.Object));
    }
}