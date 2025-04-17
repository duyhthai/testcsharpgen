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
        controller.Mediator = new Mock<IMediator>().Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(null));
    }

    [Fact]
    public async Task HandleRequest_WithInvalidRequestType_ThrowsInvalidOperationException()
    {
        // Arrange
        var controller = new AuthController();
        controller.Mediator = new Mock<IMediator>().Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(new object()));
    }

    [Fact]
    public async Task HandleRequest_WithSuccessfulOperationResult_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new AuthController();
        controller.Mediator = new Mock<IMediator>();
        controller.Mediator.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(new OperationResult { Success = true });

        // Act
        var result = await controller.HandleRequest(new ForgotPasswordCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}