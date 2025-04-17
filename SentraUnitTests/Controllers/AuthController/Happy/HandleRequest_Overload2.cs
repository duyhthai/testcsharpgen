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
    public async Task HandleRequest_WithValidForgotPasswordCommand_ReturnsOk()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<ForgotPasswordCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new ForgotPasswordCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task HandleRequest_WithValidLoginCommand_ReturnsOk()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new LoginCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task HandleRequest_WithValidRefreshTokenCommand_ReturnsOk()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new RefreshTokenCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}