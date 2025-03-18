using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockConfig = new Mock<IConfiguration>();
        _mockConfig.Setup(c => c.GetConnectionString("DefaultConnection")).Returns("YourConnectionStringHere");
        _controller = new ProductController(_mockConfig.Object);
    }

    [Fact]
    public async Task Put_WithInvalidProductId_ReturnsNotFound()
    {
        // Arrange
        int invalidId = -1;
        var product = new Product { Sku = "ABC123", Content = "Sample content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act
        var result = await _controller.Put(invalidId, product);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Put_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        int validId = 1;

        // Act
        var result = await _controller.Put(validId, null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Put_WithEmptySku_ReturnsBadRequest()
    {
        // Arrange
        int validId = 1;
        var product = new Product { Sku = "", Content = "Sample content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act
        var result = await _controller.Put(validId, product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}