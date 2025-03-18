using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        controller.ModelState.AddModelError("", "Invalid model state");

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Post_WithEmptySku_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "", Content = "Content", Price = 10.0m, IsActive = true, ImageUrl = "ImageUrl" };
        controller.ModelState.AddModelError("", "Invalid model state");

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Post_WithNegativePrice_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = -10.0m, IsActive = true, ImageUrl = "ImageUrl" };
        controller.ModelState.AddModelError("", "Invalid model state");

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
}