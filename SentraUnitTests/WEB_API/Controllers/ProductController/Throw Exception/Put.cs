using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController { _connectionString = "TestConnectionString" };
    }

    [Fact]
    public async Task Put_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        int invalidId = -1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(invalidId, product));
    }

    [Fact]
    public async Task Put_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        int validId = 1;
        Product nullProduct = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Put(validId, nullProduct));
    }

    [Fact]
    public async Task Put_WithEmptySku_ThrowsArgumentException()
    {
        // Arrange
        int validId = 1;
        var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(validId, product));
    }
}