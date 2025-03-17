using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IDapper> _mockDapper;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockDapper = new Mock<IDapper>();
            _controller = new ProductController();
            _controller.DatabaseContext = _mockConnection.Object;
            _controller.Dapper = _mockDapper.Object;
        }

        [Fact]
        public async Task Put_WithInvalidConnectionString_ThrowsSqlException()
        {
            // Arrange
            string invalidConnectionString = "InvalidConnectionString";
            _controller.DatabaseContext.ConnectionString = invalidConnectionString;
            var product = new Product { Id = 1, Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageURL" };

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(async () => await _controller.Put(1, product));
        }

        [Fact]
        public async Task Put_WithClosedConnection_ThrowsInvalidOperationException()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            var product = new Product { Id = 1, Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageURL" };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Put(1, product));
        }

        [Fact]
        public async Task Put_WithNonStoredProcedureCommandType_ThrowsArgumentException()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            var product = new Product { Id = 1, Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageURL" };
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                       .Throws(new ArgumentException("Invalid command type"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _controller.Put(1, product));
        }
    }
}