using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
    public async Task Get_ReturnsProducts_WhenDatabaseContainsData()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10 },
            new Product { Id = 2, Name = "Product2", Price = 20 }
        };

        _mockConnection.Setup(conn => conn.State).Returns(ConnectionState.Open);
        _mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
            .ReturnsAsync(products);

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Product1", result[0].Name);
        Assert.Equal(10, result[0].Price);
        Assert.Equal("Product2", result[1].Name);
        Assert.Equal(20, result[1].Price);
    }

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(ConnectionState.Open);
        _mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
            .ReturnsAsync(new List<Product>());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}