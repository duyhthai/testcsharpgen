using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockDbConnection = new Mock<IDbConnection>();
            _controller = new ProductController();
        }

        [Fact]
        public async Task Put_WithValidProduct_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "New content", Price = 9.99m, IsActive = true, ImageUrl = "image.jpg" };
            _mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockDbConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            _mockDbConnection.Verify(conn => conn.Open(), Times.Once());
            _mockDbConnection.Verify(conn => conn.ExecuteAsync("Update_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_WithOpenConnection_DoesNotReopen()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "New content", Price = 9.99m, IsActive = true, ImageUrl = "image.jpg" };
            _mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            _mockDbConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            _mockDbConnection.Verify(conn => conn.Open(), Times.Never());
            _mockDbConnection.Verify(conn => conn.ExecuteAsync("Update_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }
    }
}