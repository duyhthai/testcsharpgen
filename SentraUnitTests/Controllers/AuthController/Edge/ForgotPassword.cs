using HelloBear.Application.Auth.Commands.ForgotPassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class ForgotPasswordEdgeTests
    {
        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.ForgotPassword(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithEmptyEmail()
        {
            // Arrange
            var command = new ForgotPasswordCommand { Email = string.Empty };
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.ForgotPassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ForgotPassword_ReturnsBadRequestResult_WithInvalidEmailFormat()
        {
            // Arrange
            var command = new ForgotPasswordCommand { Email = "invalidemail" };
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.ForgotPassword(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}