using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class RefreshToken_Tests
    {
        [Fact]
        public async Task RefreshToken_ThrowsArgumentNullException_WhenCommandIsNull()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.RefreshToken(null));
        }

        [Fact]
        public async Task RefreshToken_ThrowsInvalidOperationException_WhenTokenIsEmpty()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new RefreshTokenCommand { Token = string.Empty };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.RefreshToken(command));
        }

        [Fact]
        public async Task RefreshToken_ThrowsInvalidOperationException_WhenTokenIsWhitespace()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new RefreshTokenCommand { Token = "   " };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.RefreshToken(command));
        }
    }
}