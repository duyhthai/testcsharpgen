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
        public async Task Login_ReturnsOkResult_WithValidCredentials()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var loginCommand = new LoginCommand { Email = "test@example.com", Password = "password" };
            var expectedResult = new OperationResult<LoginResponse> { Success = true, Data = new LoginResponse() };

            mockMediator.Setup(m => m.Send(It.Is<LoginCommand>(c => c.Email == loginCommand.Email && c.Password == loginCommand.Password), default))
                .ReturnsAsync(expectedResult);

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.Login(loginCommand);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OperationResult<LoginResponse>>(okObjectResult.Value);
            Assert.Equal(expectedResult.Success, response.Success);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WithInvalidCredentials()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var loginCommand = new LoginCommand { Email = "test@example.com", Password = "wrongpassword" };
            var expectedResult = new OperationResult<LoginResponse> { Success = false, Errors = new List<string> { "Invalid credentials" } };

            mockMediator.Setup(m => m.Send(It.Is<LoginCommand>(c => c.Email == loginCommand.Email && c.Password == loginCommand.Password), default))
                .ReturnsAsync(expectedResult);

            var controller = new AuthController(mockMediator.Object);

            // Act
            var result = await controller.Login(loginCommand);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<OperationResult<LoginResponse>>(badRequestObjectResult.Value);
            Assert.Equal(expectedResult.Success, response.Success);
            Assert.NotEmpty(response.Errors);
        }
    }
}