using HelloBear.Application.Auth.Commands.Login;
using HelloBear.Application.Common.Models.OperationResults;
using HelloBear.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.Login(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithEmptyEmail()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new LoginCommand { Email = string.Empty, Password = "password" };

            // Act
            var result = await controller.Login(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithEmptyPassword()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new LoginCommand { Email = "email@example.com", Password = string.Empty };

            // Act
            var result = await controller.Login(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}