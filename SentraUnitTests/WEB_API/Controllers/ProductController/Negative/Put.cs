using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly string _connectionString;

    public ProductControllerTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _connectionString = "TestConnectionString";
        _configurationMock.Setup(c => c.GetSection("ConnectionStrings").Value).Returns(_connectionString);
    }

    [Fact]
    public async Task Put_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        var controller = new ProductController(_configurationMock.Object);
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Put(-1, product));
    }

    [Fact]
    public async Task Put_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        var controller = new ProductController(_configurationMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Put(1, null));
    }

    [Fact]
    public async Task Put_WithEmptySku_ThrowsArgumentException()
    {
        // Arrange
        var controller = new ProductController(_configurationMock.Object);
        var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Put(1, product));
    }
}