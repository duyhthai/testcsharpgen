using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Get_ReturnsProducts()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Product1" }, new Product { Id = 2, Name = "Product2" } });

        var controller = new ProductController();
        ((IActionContextAccessor)controller).HttpContext.RequestServices.GetService(typeof(SqlConnection)).Should().Be(mockConnection.Object);

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Product1", result.First().Name);
        Assert.Equal("Product2", result.Skip(1).First().Name);
    }

    [Fact]
    public async void Get_ConnectionAlreadyOpen()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Product1" } });

        var controller = new ProductController();
        ((IActionContextAccessor)controller).HttpContext.RequestServices.GetService(typeof(SqlConnection)).Should().Be(mockConnection.Object);

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Product1", result.First().Name);
    }

    [Fact]
    public async void Get_EmptyResult()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .ReturnsAsync(new List<Product>());

        var controller = new ProductController();
        ((IActionContextAccessor)controller).HttpContext.RequestServices.GetService(typeof(SqlConnection)).Should().Be(mockConnection.Object);

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}