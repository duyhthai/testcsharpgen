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
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _controller = new ProductController();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task Put_UpdatesProductInDatabase()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockConnection.Verify(conn => conn.ExecuteAsync("Update_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
        }

        [Fact]
        public async Task Put_ReturnsNoContentOnSuccess()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}