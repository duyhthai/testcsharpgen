using HelloBear.Application.Auth.Commands.ForgotPassword;
using HelloBear.Application.Auth.Commands.Login;
using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

[Collection("AuthControllerTests")]
public class AuthControllerEdgeTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AuthController _authController;

    public AuthControllerEdgeTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _authController = new AuthController(_mediatorMock.Object);
    }

    [Fact]
    public async Task HandleRequest_WithNullRequest_ThrowsInvalidOperationException()
    {
        // Arrange
        var nullRequest = (IRequest<OperationResult>)null;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _authController.HandleRequest(nullRequest));
    }

    [Fact]
    public async Task HandleRequest_WithInvalidRequestType_ThrowsInvalidOperationException()
    {
        // Arrange
        var invalidRequest = new object();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _authController.HandleRequest(invalidRequest as IRequest<OperationResult>));
    }

    [Fact]
    public async Task HandleRequest_WithEmptyOperationResult_ReturnsBadRequest()
    {
        // Arrange
        var emptyResult = new OperationResult { Success = false, Errors = Array.Empty<string>() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(emptyResult);

        // Act
        var result = await _authController.HandleRequest(new ForgotPasswordCommand());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}