using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
    }

    [Fact]
    public async Task Get_ReturnsProduct_WhenIdIsValid()
    {
        // Arrange
        int id = 1;
        var product = new Product { Id = id, Name = "TestProduct" };
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
            .ReturnsAsync(new List<Product> { product });

        // Act
        var result = await _controller.Get(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task Get_ReturnsNull_WhenIdIsZero()
    {
        // Arrange
        int id = 0;
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
            .ReturnsAsync(new List<Product>());

        // Act
        var result = await _controller.Get(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionFails()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
            .ThrowsAsync(new SqlException("Connection failed"));

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Get(id));
    }
}