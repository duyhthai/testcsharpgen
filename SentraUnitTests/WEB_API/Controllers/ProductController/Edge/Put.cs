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
    public async Task Put_WithValidProductAndClosedConnection_ReopensConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Fact]
    public async Task Put_WithValidProductAndOpenConnection_DoesNotReopenConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
    }

    [Fact]
    public async Task Put_WithException_RethrowsException()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
    }
}