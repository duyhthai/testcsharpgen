using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class RefreshTokenTests
    {
        [Fact]
        public async Task RefreshToken_ReturnsOkResult_WithValidCommand()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), default)).ReturnsAsync(new OperationResult { Success = true });

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.RefreshToken(new RefreshTokenCommand());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var operationResult = Assert.IsType<OperationResult>(okObjectResult.Value);
            Assert.True(operationResult.Success);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithInvalidCommand()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), default)).ThrowsAsync(new InvalidOperationException("Invalid token"));

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.RefreshToken(new RefreshTokenCommand());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorDetails = Assert.IsType<ProblemDetails>(badRequestObjectResult.Value);
            Assert.Equal("Invalid token", errorDetails.Detail);
        }
    }
}