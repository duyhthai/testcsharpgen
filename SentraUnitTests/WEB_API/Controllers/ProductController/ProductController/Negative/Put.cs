using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c["ConnectionString"]).Returns("TestConnectionString");
            _controller = new ProductController(_mockConfig.Object);
        }

        [Fact]
        public async Task Put_WithInvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            int id = -1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(id, product));
        }

        [Fact]
        public async Task Put_WithNullProduct_ShouldThrowArgumentNullException()
        {
            // Arrange
            int id = 1;
            Product product = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Put(id, product));
        }

        [Fact]
        public async Task Put_WithEmptySku_ShouldReturnBadRequest()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}