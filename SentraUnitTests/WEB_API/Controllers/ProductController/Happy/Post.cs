using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
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
            _controller = new ProductController(_mockConnection.Object, _mockDapper.Object);
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsNewProductId()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open());
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(1, result);
            _mockConnection.Verify(conn => conn.Open(), Times.Once);
            _mockDapper.Verify(dapper => dapper.ExecuteAsync(It.Is<string>(s => s == "Create_Product"), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.Is<System.Data.CommandType>(c => c == System.Data.CommandType.StoredProcedure)), Times.Once);
        }

        [Fact]
        public async Task Post_WithOpenConnection_DoesNotReopenConnection()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(1, result);
            _mockConnection.Verify(conn => conn.Open(), Times.Never);
            _mockDapper.Verify(dapper => dapper.ExecuteAsync(It.Is<string>(s => s == "Create_Product"), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.Is<System.Data.CommandType>(c => c == System.Data.CommandType.StoredProcedure)), Times.Once);
        }

        [Fact]
        public async Task Post_WithException_RethrowsException()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => _controller.Post(product));
        }
    }
}