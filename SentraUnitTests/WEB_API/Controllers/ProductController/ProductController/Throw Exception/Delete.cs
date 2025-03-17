using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data.SqlClient;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async void Delete_WithInvalidConnectionString_ShouldThrowException()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")).Returns(new Mock<IConfigurationSection>().Object);
            mockConfiguration.Object["DefaultConnection"] = "invalid_connection_string";

            var controller = new ProductController(mockConfiguration.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(async () => await controller.Delete(1));
        }

        [Fact]
        public async void Delete_WithClosedConnection_ShouldOpenConnection()
        {
            // Arrange
            var mockSqlConnection = new Mock<SqlConnection>();
            mockSqlConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockSqlConnection.Setup(conn => conn.Open()).Verifiable();

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")).Returns(new Mock<IConfigurationSection>().Object);
            mockConfiguration.Object["DefaultConnection"] = "valid_connection_string";

            var controller = new ProductController(mockConfiguration.Object)
            {
                _connectionString = "valid_connection_string"
            };

            // Act
            await controller.Delete(1);

            // Assert
            mockSqlConnection.Verify(conn => conn.Open(), Times.Once());
        }

        [Fact]
        public async void Delete_WithValidInput_ShouldExecuteStoredProcedure()
        {
            // Arrange
            var mockSqlConnection = new Mock<SqlConnection>();
            mockSqlConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            mockSqlConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandBehavior?>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
                              .ReturnsAsync(1).Verifiable();

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")).Returns(new Mock<IConfigurationSection>().Object);
            mockConfiguration.Object["DefaultConnection"] = "valid_connection_string";

            var controller = new ProductController(mockConfiguration.Object)
            {
                _connectionString = "valid_connection_string"
            };

            // Act
            await controller.Delete(1);

            // Assert
            mockSqlConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandBehavior?>(), It.IsAny<int?>(), It.IsAny<CommandType>()), Times.Once());
        }
    }
}