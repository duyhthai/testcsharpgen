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
            _controller = new ProductController();
            _controller.DatabaseContext = _mockConnection.Object;
            _controller.Dapper = _mockDapper.Object;
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsNewProductId()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var expectedResult = 1;

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockDapper.Verify(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        }

        [Fact]
        public async Task Post_WithOpenConnection_ReturnsNewProductId()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var expectedResult = 1;

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockConnection.Verify(conn => conn.Open(), Times.Never());
            _mockDapper.Verify(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        }

        [Fact]
        public async Task Post_WithException_ThrowsException()
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