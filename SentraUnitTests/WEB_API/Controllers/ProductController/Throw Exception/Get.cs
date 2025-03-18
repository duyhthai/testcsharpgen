using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly string _connectionString = "YourConnectionStringHere";

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
        }

        [Fact]
        public async Task Get_ThrowsException_WhenConnectionIsNull()
        {
            // Arrange
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get());
        }

        [Fact]
        public async Task Get_ThrowsException_WhenConnectionOpenFails()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException("Failed to open connection"));
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
            controller._connectionString = _connectionString;
            controller._dbConnectionFactory = () => _mockConnection.Object;

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get());
        }

        [Fact]
        public async Task Get_ThrowsException_WhenQueryExecutionFails()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType>())).Throws(new SqlException("Query execution failed"));
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
            controller._connectionString = _connectionString;
            controller._dbConnectionFactory = () => _mockConnection.Object;

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get());
        }
    }
}