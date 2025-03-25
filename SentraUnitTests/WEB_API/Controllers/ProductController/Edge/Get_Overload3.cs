using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsException_WithEmptyConnectionString()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = string.Empty;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, "test"));
    }

    [Fact]
    public async Task Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "valid_connection_string";

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(-1, "test"));
    }

    [Fact]
    public async Task Get_ThrowsException_WithInvalidName()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "valid_connection_string";

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, null));
    }
}