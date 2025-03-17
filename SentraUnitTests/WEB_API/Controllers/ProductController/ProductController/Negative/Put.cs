using Microsoft.AspNetCore.Mvc;
using Moq;
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
            _controller.ControllerContext = new ControllerContext(new DefaultHttpContext());
        }

        [Fact]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = -1;
            var product = new Product { Sku = "ABC123", Content = "Sample content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _controller.ModelState.AddModelError("id", "The Id field must be greater than 0.");

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The Id field must be greater than 0.", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task Put_WithNullProduct_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            _controller.ModelState.AddModelError("product", "Product cannot be null.");

            // Act
            var result = await _controller.Put(id, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Product cannot be null.", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task Put_WithClosedConnectionThrowsException()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "ABC123", Content = "Sample content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Throws(new InvalidOperationException("Connection closed."));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Put(id, product));
        }
    }
}