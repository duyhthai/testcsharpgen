using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
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
        _controller = new ProductController(_mockConnection.Object);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [Fact]
    public async Task Put_WithValidProduct_ReturnsNoContent()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "New content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockConnection.Setup(conn => conn.CreateCommand()).Returns(new SqlCommand());
        _mockConnection.Setup(conn => conn.CreateParameter()).Returns(new SqlParameter());

        // Act
        var result = await _controller.Put(id, product);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Fact]
    public async Task Put_WithClosedConnection_OpensConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "New content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Fact]
    public async Task Put_WithOpenConnection_DoesNotOpenConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "New content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}