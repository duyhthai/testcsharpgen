using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Get_ThrowsException_WithEmptyConnectionString()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = string.Empty;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, "test"));
    }

    [Fact]
    public async void Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "valid_connection_string";

        // Mocking the database connection to throw an exception when Open() is called
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(-1, "test"));
    }

    [Fact]
    public async void Get_ThrowsException_WithInvalidName()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "valid_connection_string";

        // Mocking the database connection to throw an exception when Open() is called
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, null));
    }
}