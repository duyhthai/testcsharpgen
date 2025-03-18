using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController { _connectionString = "TestConnectionString" };
        _controller.Database = _mockConnection.Object;
    }

    [Test]
    public async Task Put_WithValidProduct_ReturnsNoContentResult()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Mock the connection behavior
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        var result = await _controller.Put(id, product);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once());
        _mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
    }

    [Test]
    public async Task Put_WithInvalidId_ReturnsBadRequestObjectResult()
    {
        // Arrange
        int id = -1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Mock the connection behavior
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        var result = await _controller.Put(id, product);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Never());
        _mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Never());
    }

    [Test]
    public async Task Put_WithOpenConnection_DoesNotReopen()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Mock the connection behavior
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        var result = await _controller.Put(id, product);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Never());
        _mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
    }
}