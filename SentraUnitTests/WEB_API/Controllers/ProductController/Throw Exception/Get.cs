using Microsoft.AspNetCore.Mvc;
using Moq;
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
        _controller = new ProductController { _connectionString = "ValidConnectionString" };
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
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, CommandType.Text)).ThrowsAsync(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Get());
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionIsClosed()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get());
    }
}