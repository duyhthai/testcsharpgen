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
        var controller = new ProductController(null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get(1, "test"));
    }

    [Fact]
    public async Task Get_ThrowsException_WithNonExistentStoredProcedure()
    {
        // Arrange
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var controller = new ProductController(connectionString);

        using (var mockConnection = new MockSqlConnection())
        {
            controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, "test"));
        }
    }

    [Fact]
    public async Task Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var controller = new ProductController(connectionString);

        using (var mockConnection = new MockSqlConnection())
        {
            controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(-1, "test"));
        }
    }
}