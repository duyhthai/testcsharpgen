using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(c => c["ConnectionStrings:DefaultConnection"]).Returns("YourConnectionStringHere");
            _controller = new ProductController(_configurationMock.Object);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldCallExecuteAsyncOnce()
        {
            // Arrange
            int validId = 1;
            var mockDbConnection = new Mock<SqlConnection>();
            mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockDbConnection.Setup(conn => conn.Open()).Verifiable();
            mockDbConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));

            _controller.DatabaseContext = mockDbConnection.Object;

            // Act
            await _controller.Delete(validId);

            // Assert
            mockDbConnection.Verify(conn => conn.Open(), Times.Once());
            mockDbConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Once());
        }

        [Fact]
        public async Task Delete_WithInvalidId_ShouldNotCallExecuteAsync()
        {
            // Arrange
            int invalidId = -1;
            var mockDbConnection = new Mock<SqlConnection>();
            mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockDbConnection.Setup(conn => conn.Open()).Verifiable();

            _controller.DatabaseContext = mockDbConnection.Object;

            // Act
            await _controller.Delete(invalidId);

            // Assert
            mockDbConnection.Verify(conn => conn.Open(), Times.Never());
            mockDbConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Never());
        }

        [Fact]
        public async Task Delete_WithClosedConnection_ShouldOpenConnectionBeforeExecuting()
        {
            // Arrange
            int validId = 1;
            var mockDbConnection = new Mock<SqlConnection>();
            mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockDbConnection.Setup(conn => conn.Open()).Verifiable();
            mockDbConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));

            _controller.DatabaseContext = mockDbConnection.Object;

            // Act
            await _controller.Delete(validId);

            // Assert
            mockDbConnection.Verify(conn => conn.Open(), Times.Once());
            mockDbConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Once());
        }
    }
}