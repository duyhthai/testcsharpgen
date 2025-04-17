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
    public async Task HandleRequest_WithValidForgotPasswordCommand_ReturnsOkObjectResult()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<ForgotPasswordCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new ForgotPasswordCommand());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okResult.Value);
        Assert.True(operationResult.Success);
    }

    [Fact]
    public async Task HandleRequest_WithValidLoginCommand_ReturnsOkObjectResult()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new LoginCommand());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okResult.Value);
        Assert.True(operationResult.Success);
    }

    [Fact]
    public async Task HandleRequest_WithValidRefreshTokenCommand_ReturnsOkObjectResult()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.HandleRequest(new RefreshTokenCommand());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okResult.Value);
        Assert.True(operationResult.Success);
    }
}