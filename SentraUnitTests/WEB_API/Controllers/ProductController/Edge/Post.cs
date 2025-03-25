using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

[Collection("DatabaseTests")]
public class ProductControllerEdgeTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDapper> _mockDapper;
    private readonly ProductController _controller;

    public ProductControllerEdgeTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Fact]
    public async Task Post_WithEmptySku_ReturnsBadRequest()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "Test content", Price = 100, IsActive = true, ImageUrl = "test.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act & Assert
        var result = await _controller.Post(product);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task Post_WithNegativePrice_ReturnsBadRequest()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Test content", Price = -100, IsActive = true, ImageUrl = "test.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act & Assert
        var result = await _controller.Post(product);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task Post_WithClosedConnection_ReturnsSuccess()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Test content", Price = 100, IsActive = true, ImageUrl = "test.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var idResult = (int)((OkObjectResult)result).Value;
        Assert.NotEqual(0, idResult);
    }
}