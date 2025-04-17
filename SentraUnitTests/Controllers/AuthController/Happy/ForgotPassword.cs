using HelloBear.Application.Auth.Commands.ForgotPassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public async Task ForgotPassword_ReturnsOkResult_WithValidCommand()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<ForgotPasswordCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new OperationResult { Success = true });

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.ForgotPassword(new ForgotPasswordCommand { Email = "test@example.com" });

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ForgotPasswordResponse>(okObjectResult.Value);
        Assert.True(response.Success);
    }

    [Fact]
    public async Task ForgotPassword_ReturnsBadRequestResult_WithInvalidCommand()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<ForgotPasswordCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Invalid command"));

        var controller = new AuthController(mockMediator.Object);

        // Act
        var result = await controller.ForgotPassword(new ForgotPasswordCommand { Email = "" });

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestObjectResult.Value);
        Assert.Equal("Invalid command", errorResponse.Message);
    }
}