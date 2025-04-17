using HelloBear.Application.Auth.Commands.Login;
using HelloBear.Application.Common.Models.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloBear.Tests.Controllers.AuthControllerTests
{
    public class LoginNegativeTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public LoginNegativeTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithNullCommand()
        {
            // Arrange
            LoginCommand nullCommand = null;

            // Act
            var result = await _controller.Login(nullCommand);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithEmptyEmail()
        {
            // Arrange
            var command = new LoginCommand { Email = "", Password = "password" };

            // Act
            var result = await _controller.Login(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithEmptyPassword()
        {
            // Arrange
            var command = new LoginCommand { Email = "email@example.com", Password = "" };

            // Act
            var result = await _controller.Login(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}