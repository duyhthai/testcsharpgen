using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly string _connectionString = "YourConnectionStringHere";

        [Fact]
        public async Task Put_WithValidProduct_ReturnsNoContent()
        {
            // Arrange
            var controller = new ProductController();
            var product = new Product { Sku = "SKU123", Content = "Test content", Price = 19.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

            // Act
            var result = await controller.Put(1, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_WithInvalidProduct_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ProductController();
            var product = new Product { Sku = "", Content = null, Price = -1m, IsActive = false, ImageUrl = "" };

            // Act
            var result = await controller.Put(1, product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var controller = new ProductController();
            var product = new Product { Sku = "SKU456", Content = "Another test content", Price = 29.99m, IsActive = true, ImageUrl = "http://example.com/another-image.jpg" };

            // Act
            var result = await controller.Put(999, product);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}