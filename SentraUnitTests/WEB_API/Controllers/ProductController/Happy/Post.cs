using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Post_WithValidProduct_ReturnsNewProductId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        var controller = new ProductController { _connectionString = "FakeConnectionString" };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var result = await controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        Assert.Equal(1, result);
    }

    [Fact]
    public async void Post_WithNullProduct_ReturnsZero()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "FakeConnectionString" };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async void Post_WithOpenConnection_DoesNotReopen()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        var controller = new ProductController { _connectionString = "FakeConnectionString" };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        // Act
        var result = await controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Never());
        mockConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        Assert.Equal(1, result);
    }
}