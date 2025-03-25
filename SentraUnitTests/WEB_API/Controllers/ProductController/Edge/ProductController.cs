using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsEmptyList_WhenNoProductsFound()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                      .ReturnsAsync(new List<Product>());

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenProductNotFound()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                      .ReturnsAsync(new List<Product>());

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Post_CreatesNewProductAndReturnsId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                      .ReturnsAsync(1);

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.Equal(1, result);
    }
}