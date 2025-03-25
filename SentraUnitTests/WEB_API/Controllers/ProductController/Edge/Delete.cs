using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_ShouldExecuteStoredProcedureWithValidId()
    {
        // Arrange
        var controller = new ProductController();
        var id = 1;
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        controller._connectionString = connectionString;

        // Act
        await controller.Delete(id);

        // Assert
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var result = await conn.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Products WHERE Id = @id", new { id });
            Assert.Equal(0, result);
        }
    }

    [Fact]
    public async Task Delete_ShouldNotOpenConnectionIfAlreadyOpen()
    {
        // Arrange
        var controller = new ProductController();
        var id = 1;
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        controller._connectionString = connectionString;
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
        }

        // Act
        await controller.Delete(id);

        // Assert
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var result = await conn.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Products WHERE Id = @id", new { id });
            Assert.Equal(0, result);
        }
    }

    [Fact]
    public async Task Delete_ShouldThrowArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController();
        var id = -1;
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        controller._connectionString = connectionString;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(id));
    }
}