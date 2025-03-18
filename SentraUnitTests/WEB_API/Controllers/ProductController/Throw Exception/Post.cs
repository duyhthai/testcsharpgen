using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Post_WithNullProduct_ShouldThrowArgumentNullException()
    {
        // Arrange
        var controller = new ProductController();
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        controller.ControllerContext = new ControllerContext { HttpContext = mockHttpContextAccessor.Object };
        controller.ModelState.AddModelError("", "Model state error");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Post(null));
    }

    [Fact]
    public async void Post_WithInvalidConnectionString_ShouldThrowSqlException()
    {
        // Arrange
        var controller = new ProductController();
        controller._connectionString = "Invalid Connection String";
        var product = new Product { Sku = "123", Content = "Test", Price = 10.0m, IsActive = true, ImageUrl = "test.jpg" };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Post(product));
    }

    [Fact]
    public async void Post_WithOpenConnection_ShouldReturnNewId()
    {
        // Arrange
        var controller = new ProductController();
        controller._connectionString = "Valid Connection String";
        var product = new Product { Sku = "123", Content = "Test", Price = 10.0m, IsActive = true, ImageUrl = "test.jpg" };
        var mockSqlConnection = new Mock<SqlConnection>(controller._connectionString);
        mockSqlConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockSqlConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandBehavior?>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
                          .ReturnsAsync(1);

        controller.DatabaseContext = mockSqlConnection.Object;

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.Equal(1, result);
    }
}