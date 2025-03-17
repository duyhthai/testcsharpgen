using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.SetupGet(c => c["ConnectionString"]).Returns("Server=.;Database=TestDB;Trusted_Connection=True;");
            _controller = new ProductController(_mockConfig.Object);
        }

        [Fact]
        public async Task Put_WithValidProduct_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

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
            var product = new Product { Sku = "", Content = "", Price = -1, IsActive = false, ImageUrl = "" };

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int id = 1000;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
            _controller.ControllerContext = new ControllerContext(new DefaultHttpContext(), new RouteData(), _controller);

            // Act
            var result = await _controller.Put(id, product);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}