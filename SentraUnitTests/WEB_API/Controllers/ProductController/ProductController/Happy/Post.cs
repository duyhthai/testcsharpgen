using Microsoft.AspNetCore.Mvc;
using Moq;
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
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Fact]
    public async Task Post_WithValidProduct_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        var parameters = new DynamicParameters();
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);
        parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.Equal(1, result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockDapper.Verify(dapper => dapper.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Fact]
    public async Task Post_WithOpenConnection_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        var parameters = new DynamicParameters();
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);
        parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockDapper.Setup(dapper => dapper.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.Equal(1, result);
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
        _mockDapper.Verify(dapper => dapper.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }
}