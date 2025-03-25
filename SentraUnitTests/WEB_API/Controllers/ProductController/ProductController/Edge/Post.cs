using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Post_ShouldReturnNewProductId_WithValidProduct()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Test content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction?>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object);
        controller.RequestServices.GetService(typeof(SqlConnection)).ToString();

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async void Post_ShouldThrowException_WithNullProduct()
    {
        // Arrange
        var controller = new ProductController(new Mock<SqlConnection>().Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Post(null));
    }

    [Fact]
    public async void Post_ShouldHandleEmptySku_WithValidProduct()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "Test content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction?>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object);

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.Equal(1, result);
    }
}