using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class UpdatePassword_Tests
    {
        [Fact]
        public async Task UpdatePassword_WithNullCommand_ThrowsArgumentNullException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new AuthController(mediatorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.UpdatePassword(null));
        }

        [Fact]
        public async Task UpdatePassword_WithEmptyEmail_ThrowsArgumentException()
        {
            // Arrange
            var command = new UpdatePasswordCommand { Email = "", NewPassword = "newpassword" };
            var mediatorMock = new Mock<IMediator>();
            var controller = new AuthController(mediatorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.UpdatePassword(command));
        }

        [Fact]
        public async Task UpdatePassword_WithShortNewPassword_ThrowsArgumentException()
        {
            // Arrange
            var command = new UpdatePasswordCommand { Email = "test@example.com", NewPassword = "short" };
            var mediatorMock = new Mock<IMediator>();
            var controller = new AuthController(mediatorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.UpdatePassword(command));
        }
    }
}