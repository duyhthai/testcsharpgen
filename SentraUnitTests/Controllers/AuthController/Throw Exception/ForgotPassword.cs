using HelloBear.Application.Auth.Commands.ForgotPassword;
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
        public async Task ForgotPassword_ThrowsArgumentNullException_WhenCommandIsNull()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.ForgotPassword(null));
        }

        [Fact]
        public async Task ForgotPassword_ThrowsInvalidOperationException_WhenCommandIsInvalid()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AuthController(mockMediator.Object);
            var command = new ForgotPasswordCommand { Email = null };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.ForgotPassword(command));
        }
    }
}