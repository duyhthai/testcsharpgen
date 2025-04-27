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
        var controller = new AuthController(new Mock<IMediator>().Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(null));
    }

    [Fact]
    public async Task HandleRequest_WithInvalidRequestType_ThrowsInvalidOperationException()
    {
        // Arrange
        var controller = new AuthController(new Mock<IMediator>().Object);
        var invalidRequest = new object();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.HandleRequest(invalidRequest));
    }

    [Fact]
    public async Task HandleRequest_WithEmptyOperationResult_ReturnsBadRequest()
    {
        // Arrange
        var controller = new AuthController(new Mock<IMediator>().Object);
        var emptyResult = new OperationResult { Success = false, Errors = Array.Empty<string>() };

        // Act
        var result = await controller.HandleRequest(emptyResult);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}