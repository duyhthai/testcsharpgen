using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.SetupGet(c => c["ConnectionString"]).Returns("TestConnectionString");
        _controller = new ProductController(_configurationMock.Object);
    }

    [Fact]
    public async Task Post_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        Product product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_WithInvalidSku_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Sku = "invalid_sku" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_WithNegativePrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var product = new Product { Price = -10m };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _controller.Post(product));
    }
}