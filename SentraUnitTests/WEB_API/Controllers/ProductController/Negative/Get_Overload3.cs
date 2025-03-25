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
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, "Test"));
    }

    [Fact]
    public async Task Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        var parameters = new DynamicParameters();
        parameters.Add("@id", -1);
        parameters.Add("@name", "Test");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(-1, "Test"));
    }

    [Fact]
    public async Task Get_ThrowsException_WithInvalidName()
    {
        // Arrange
        var controller = new ProductController();
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        parameters.Add("@name", null);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, null));
    }
}