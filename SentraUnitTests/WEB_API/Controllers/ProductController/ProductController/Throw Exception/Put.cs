using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _controller = new ProductController(_mockConnectionString);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = _mockHttpContextAccessor.Object.HttpContext
        };
    }

    [Fact]
    public async void Put_WithInvalidConnectionString_ThrowsSqlException()
    {
        // Arrange
        var mockConnectionStringProvider = new Mock<IConnectionStringProvider>();
        mockConnectionStringProvider.Setup(p => p.GetConnectionString()).Returns(() => throw new SqlException());
        _controller.ConnectionStringProvider = mockConnectionStringProvider.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Put(1, new Product()));
    }

    [Fact]
    public async void Put_WithClosedConnection_ThrowsInvalidOperationException()
    {
        // Arrange
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        parameters.Add("@sku", "SKU1");
        parameters.Add("@content", "Content1");
        parameters.Add("@price", 10.00m);
        parameters.Add("@isActive", true);
        parameters.Add("@imageUrl", "image1.jpg");

        _mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(c => c.Open());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Put(1, new Product()));
    }

    [Fact]
    public async void Put_WithOpenConnection_Succeeds()
    {
        // Arrange
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        parameters.Add("@sku", "SKU1");
        parameters.Add("@content", "Content1");
        parameters.Add("@price", 10.00m);
        parameters.Add("@isActive", true);
        parameters.Add("@imageUrl", "image1.jpg");

        _mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Put(1, new Product());

        // Assert
        _mockConnection.Verify(c => c.Open(), Times.Once);
        _mockConnection.Verify(c => c.ExecuteAsync("Update_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }
}