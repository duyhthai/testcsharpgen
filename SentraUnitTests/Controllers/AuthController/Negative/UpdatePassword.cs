using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class UpdatePassword_Negative_Tests
    {
        [Fact]
        public async Task UpdatePassword_WithNullCommand_ReturnsBadRequest()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.UpdatePassword(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePassword_WithEmptyEmail_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdatePasswordCommand { Email = "", NewPassword = "newpassword" };
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.UpdatePassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePassword_WithShortNewPassword_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdatePasswordCommand { Email = "test@example.com", NewPassword = "short" };
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.UpdatePassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}