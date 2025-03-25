using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
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
    public async Task Get_ReturnsEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text)).ReturnsAsync(new List<Product>());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionStringIsNull()
    {
        // Arrange
        _controller._connectionString = null;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get());
    }

    [Fact]
    public async Task Get_ThrowsException_WhenQueryFails()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text)).ThrowsAsync(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Get());
    }
}