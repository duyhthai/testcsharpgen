using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDapper> _mockDapper;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Fact]
    public async Task Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var request = null as Product;

        // Act
        var response = await _controller.Post(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task Post_WithInvalidSku_ReturnsBadRequest()
    {
        // Arrange
        var product = new Product { Sku = "invalid_sku", Content = "test content", Price = 10.0m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var response = await _controller.Post(product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task Post_WithNegativePrice_ReturnsBadRequest()
    {
        // Arrange
        var product = new Product { Sku = "valid_sku", Content = "test content", Price = -10.0m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var response = await _controller.Post(product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }
}