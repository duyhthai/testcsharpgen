using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsExceptionForEmptyConnectionString()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "" };
        int id = 1;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(id));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForInvalidIdType()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;" };
        string invalidId = "abc";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(invalidId));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForClosedConnection()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;" };
        int id = 1;
        using (var conn = new SqlConnection(controller._connectionString))
        {
            conn.Close();
        }

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(id));
    }
}