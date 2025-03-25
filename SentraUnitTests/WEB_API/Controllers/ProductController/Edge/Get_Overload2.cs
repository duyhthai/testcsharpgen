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
        var controller = new ProductController(null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForInvalidIdType()
    {
        // Arrange
        var controller = new ProductController("valid_connection_string");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(-1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionForClosedConnection()
    {
        // Arrange
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var controller = new ProductController(connectionString);
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Close();
        }

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
    }
}