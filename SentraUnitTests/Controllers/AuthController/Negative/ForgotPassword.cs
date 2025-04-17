using HelloBear.Application.Auth.Commands.ForgotPassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class ForgotPasswordNegativeTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public ForgotPasswordNegativeTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            ForgotPasswordCommand nullCommand = null;

            // Act
            var result = await _controller.ForgotPassword(nullCommand);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithEmptyEmail()
        {
            // Arrange
            var command = new ForgotPasswordCommand { Email = string.Empty };

            // Act
            var result = await _controller.ForgotPassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithInvalidEmailFormat()
        {
            // Arrange
            var command = new ForgotPasswordCommand { Email = "invalidemail" };

            // Act
            var result = await _controller.ForgotPassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}