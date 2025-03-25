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
    public async Task Post_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        var request = null as Product;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Post(request));
    }

    [Fact]
    public async Task Post_WithInvalidSku_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Sku = null };

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