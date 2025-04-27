using HelloBear.Application.Auth.Commands.Login;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class ThrowExceptionTests
    {
        [Fact]
        public async Task Login_ThrowsArgumentNullException_WhenLoginCommandIsNull()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Login(null));
        }

        [Fact]
        public async Task Login_ThrowsArgumentException_WhenEmailIsEmpty()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new LoginCommand { Email = string.Empty, Password = "password" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.Login(command));
        }

        [Fact]
        public async Task Login_ThrowsArgumentException_WhenPasswordIsEmpty()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new LoginCommand { Email = "email@example.com", Password = string.Empty };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.Login(command));
        }
    }
}