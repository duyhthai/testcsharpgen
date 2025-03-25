using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsExceptionForInvalidConnectionString()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "invalid_connection_string";
        var mockDapper = new Mock<IDapper>();
        controller.Database = mockDapper.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForEmptyConnectionString()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = string.Empty;
        var mockDapper = new Mock<IDapper>();
        controller.Database = mockDapper.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForInvalidIdType()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "valid_connection_string";
        var mockDapper = new Mock<IDapper>();
        controller.Database = mockDapper.Object;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(-1));
    }
}