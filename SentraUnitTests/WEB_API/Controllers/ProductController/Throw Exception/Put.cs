using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly ProductController _controller;
    private readonly string _connectionString = "TestConnectionString";

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _controller = new ProductController();
        _controller.ControllerContext = new ControllerContext(new DefaultHttpContext(), null, null);
        _controller._connectionString = _connectionString;
    }

    [Fact]
    public async void Put_ThrowsException_WhenDatabaseOperationFails()
    {
        // Arrange
        var productId = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Put(productId, product));
    }

    [Fact]
    public async void Put_ThrowsArgumentNullException_WhenProductIsNull()
    {
        // Arrange
        var productId = 1;
        Product product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Put(productId, product));
    }

    [Fact]
    public async void Put_ThrowsArgumentException_WhenIdIsZero()
    {
        // Arrange
        var productId = 0;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(productId, product));
    }
}