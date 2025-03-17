using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
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
    }

    [Fact]
    public async Task Put_ThrowsException_WhenDatabaseConnectionFails()
    {
        // Arrange
        var id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
    }

    [Fact]
    public async Task Put_ThrowsException_WhenStoredProcedureExecutionFails()
    {
        // Arrange
        var id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
    }
}