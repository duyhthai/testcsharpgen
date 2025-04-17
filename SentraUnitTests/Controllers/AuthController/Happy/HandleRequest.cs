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
        mockMediator.Setup(m => m.Send(It.IsAny<ForgotPasswordCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController();
        controller.Mediator = mockMediator.Object;

        // Act
        var result = await controller.HandleRequest(new ForgotPasswordCommand());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okObjectResult.Value);
        Assert.True(operationResult.Success);
    }

    [Fact]
    public async Task HandleRequest_WithValidLoginCommand_ReturnsOk()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController();
        controller.Mediator = mockMediator.Object;

        // Act
        var result = await controller.HandleRequest(new LoginCommand());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okObjectResult.Value);
        Assert.True(operationResult.Success);
    }

    [Fact]
    public async Task HandleRequest_WithValidRefreshTokenCommand_ReturnsOk()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController();
        controller.Mediator = mockMediator.Object;

        // Act
        var result = await controller.HandleRequest(new RefreshTokenCommand());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var operationResult = Assert.IsType<OperationResult>(okObjectResult.Value);
        Assert.True(operationResult.Success);
    }
}