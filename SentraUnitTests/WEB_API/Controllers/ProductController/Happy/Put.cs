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
        public async Task Put_WithValidProduct_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Test content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_WithOpenConnection_DoesNotReopenConnection()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Test content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Never());
        }

        [Fact]
        public async Task Put_WithValidParameters_CallsExecuteAsyncOnce()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Test content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
        }
    }
}