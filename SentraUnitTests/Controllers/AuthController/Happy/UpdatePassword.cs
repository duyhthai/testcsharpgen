using HelloBear.Application.Auth.Commands.UpdatePassword;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class UpdatePasswordTests
    {
        [Fact]
        public async Task UpdatePassword_WithValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdatePasswordCommand>(), default)).ReturnsAsync(new OperationResult { Success = true });

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.UpdatePassword(new UpdatePasswordCommand());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OperationResult>(okObjectResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task UpdatePassword_WithInvalidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdatePasswordCommand>(), default)).ThrowsAsync(new InvalidOperationException("Invalid command"));

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.UpdatePassword(new UpdatePasswordCommand());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ProblemDetails>(badRequestObjectResult.Value);
            Assert.Equal("Invalid command", errorResponse.Detail);
        }
    }
}