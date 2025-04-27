using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    [Collection("AuthControllerTests")]
    public class UpdatePasswordEdgeTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public UpdatePasswordEdgeTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task UpdatePassword_WithNullCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = null as UpdatePasswordCommand;

            // Act
            var result = await _controller.UpdatePassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePassword_WithEmptyEmail_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdatePasswordCommand { Email = string.Empty };

            // Act
            var result = await _controller.UpdatePassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePassword_WithShortNewPassword_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdatePasswordCommand { NewPassword = "short" };

            // Act
            var result = await _controller.UpdatePassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}