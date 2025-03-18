using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
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
        _mockConfig.Setup(config => config.GetConnectionString("DefaultConnection")).Returns("YourConnectionStringHere");
        _controller = new ProductController(_mockConfig.Object);
    }

    [Fact]
    public async Task Put_InvalidId_ReturnsBadRequest()
    {
        // Arrange
        int invalidId = -1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.00m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var result = await _controller.Put(invalidId, product);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid ID", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task Put_NullProduct_ReturnsBadRequest()
    {
        // Arrange
        int validId = 1;
        Product nullProduct = null;

        // Act
        var result = await _controller.Put(validId, nullProduct);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Product cannot be null", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task Put_ConnectionFailed_ReturnsInternalServerError()
    {
        // Arrange
        int validId = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.00m, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act
        var result = await _controller.Put(validId, product);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, internalServerErrorResult.StatusCode);
    }
}