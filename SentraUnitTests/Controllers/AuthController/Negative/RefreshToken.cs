using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class RefreshToken_Negative_Tests
    {
        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.RefreshToken(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithEmptyToken()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new RefreshTokenCommand { Token = string.Empty };

            // Act
            var result = await controller.RefreshToken(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithWhitespaceToken()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new RefreshTokenCommand { Token = "   " };

            // Act
            var result = await controller.RefreshToken(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}