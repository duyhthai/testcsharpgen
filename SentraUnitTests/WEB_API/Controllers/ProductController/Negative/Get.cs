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
        _controller = new ProductController();
        _controller.DatabaseContext = new Mock<IDatabaseContext>().Object;
    }

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenNoProductsFound()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, CommandType.Text)).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionStringIsNull()
    {
        // Arrange
        _controller.DatabaseContext.ConnectionString = null;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get());
    }

    [Fact]
    public async Task Get_ThrowsException_WhenDatabaseQueryFails()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, CommandType.Text)).ThrowsAsync(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Get());
    }
}