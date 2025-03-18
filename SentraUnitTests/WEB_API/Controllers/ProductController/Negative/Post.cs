using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Post_WithInvalidSku_ReturnsConflict()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "invalid_sku" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async void Post_WithNegativePrice_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Price = -10.0m };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}