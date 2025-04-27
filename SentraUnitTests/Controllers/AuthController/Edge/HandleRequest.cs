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
    public async Task HandleRequest_WithEmptyRequest_ThrowsInvalidOperationException()
    {
        // Arrange
        var emptyRequest = null as IRequest<OperationResult>;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _authController.HandleRequest(emptyRequest));
    }

    [Fact]
    public async Task HandleRequest_WithUnsupportedRequestType_ThrowsInvalidOperationException()
    {
        // Arrange
        var unsupportedRequest = new object() as IRequest<OperationResult>;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _authController.HandleRequest(unsupportedRequest));
    }

    [Fact]
    public async Task HandleRequest_WithSuccessfulOperationResult_ReturnsOkObjectResult()
    {
        // Arrange
        var successfulResult = new OperationResult { Success = true };
        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<OperationResult>>())).ReturnsAsync(successfulResult);

        // Act
        var result = await _authController.HandleRequest(new ForgotPasswordCommand());

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal(successfulResult, okResult.Value);
    }
}