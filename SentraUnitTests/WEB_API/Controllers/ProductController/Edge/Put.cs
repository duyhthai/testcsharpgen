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
    public async Task Put_WithNegativeId_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(-1, product));
    }

    [Fact]
    public async Task Put_WithEmptySku_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(1, product));
    }

    [Fact]
    public async Task Put_WithClosedConnection_OpensConnection()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act
        await _controller.Put(1, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once());
    }
}