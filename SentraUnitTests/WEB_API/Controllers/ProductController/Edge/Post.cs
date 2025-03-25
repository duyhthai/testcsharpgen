using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public async Task Post_WithEmptyProduct_SetsDefaultValuesAndReturnsNewProductId()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "", Price = 0, IsActive = false, ImageUrl = "" };
        var parameters = new DynamicParameters();
        parameters.Add("@sku", "");
        parameters.Add("@content", "");
        parameters.Add("@price", 0);
        parameters.Add("@isActive", false);
        parameters.Add("@imageUrl", "");
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
    public async Task Post_WithMaxPrice_SetsCorrectValueAndReturnsNewProductId()
    {
        // Arrange
        var maxPrice = decimal.MaxValue;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = maxPrice, IsActive = true, ImageUrl = "ImageUrl" };
        var parameters = new DynamicParameters();
        parameters.Add("@sku", "SKU123");
        parameters.Add("@content", "Content");
        parameters.Add("@price", maxPrice);
        parameters.Add("@isActive", true);
        parameters.Add("@imageUrl", "ImageUrl");
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
    public async Task Post_WithMinPrice_SetsCorrectValueAndReturnsNewProductId()
    {
        // Arrange
        var minPrice = decimal.MinValue;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = minPrice, IsActive = true, ImageUrl = "ImageUrl" };
        var parameters = new DynamicParameters();
        parameters.Add("@sku", "SKU123");
        parameters.Add("@content", "Content");
        parameters.Add("@price", minPrice);
        parameters.Add("@isActive", true);
        parameters.Add("@imageUrl", "ImageUrl");
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
}