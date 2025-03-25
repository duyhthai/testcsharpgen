using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.SetupGet(c => c["ConnectionString"]).Returns("Data Source=.;Initial Catalog=TestDB;Integrated Security=True;");
        _controller = new ProductController(_configurationMock.Object);
    }

    [Fact]
    public async Task Post_WithEmptyProduct_SetsDefaultValuesAndInsertsIntoDatabase()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "", Price = 0, IsActive = false, ImageUrl = "" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        _controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await _controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync(It.Is<string>(s => s.Contains("Create_Product")), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task Post_WithMaxPriceValue_InsertsIntoDatabase()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = decimal.MaxValue, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        _controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await _controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync(It.Is<string>(s => s.Contains("Create_Product")), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task Post_WithMinPriceValue_InsertsIntoDatabase()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = decimal.MinValue, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        _controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await _controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync(It.Is<string>(s => s.Contains("Create_Product")), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()), Times.Once());
        Assert.Equal(1, result);
    }
}