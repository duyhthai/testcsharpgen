using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Data;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Post_WithValidProduct_ReturnsNewProductId()
    {
        // Arrange
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var controller = new ProductController { _connectionString = connectionString };
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsNotEmpty(result);
        Assert.Greater(result, 0);
    }

    [Test]
    public async Task Post_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => controller.Post(null));
    }

    [Test]
    public async Task Post_WithInvalidProduct_ReturnsZero()
    {
        // Arrange
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var controller = new ProductController { _connectionString = connectionString };
        var product = new Product { Sku = "", Content = null, Price = -1m, IsActive = false, ImageUrl = "" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.AreEqual(0, result);
    }
}