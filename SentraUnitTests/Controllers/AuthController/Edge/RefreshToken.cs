using HelloBear.Application.Auth.Commands.RefreshToken;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class RefreshTokenEdgeTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public RefreshTokenEdgeTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            var command = null as RefreshTokenCommand;

            // Act
            var result = await _controller.RefreshToken(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithEmptyToken()
        {
            // Arrange
            var command = new RefreshTokenCommand { Token = string.Empty };

            // Act
            var result = await _controller.RefreshToken(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RefreshToken_ReturnsBadRequestResult_WithWhitespaceToken()
        {
            // Arrange
            var command = new RefreshTokenCommand { Token = "   " };

            // Act
            var result = await _controller.RefreshToken(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}