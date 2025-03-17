using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

        public ProductControllerTests()
        {
            _controller = new ProductController();
        }

        [Fact]
        public async Task Put_WithValidProduct_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Updated content", Price = 99.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_WithInvalidProduct_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "", Content = null, Price = -1m, IsActive = false, ImageUrl = "" };

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int id = 999;
            var product = new Product { Sku = "SKU123", Content = "Updated content", Price = 99.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}