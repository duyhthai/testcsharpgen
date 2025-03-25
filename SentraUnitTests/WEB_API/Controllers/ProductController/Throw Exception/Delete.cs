using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_ShouldThrowArgumentNullException_WhenConnectionStringIsNull()
    {
        // Arrange
        var controller = new ProductController(null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Delete(1));
    }

    [Fact]
    public async Task Delete_ShouldThrowArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController("valid_connection_string");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(-1));
    }

    [Fact]
    public async Task Delete_ShouldThrowInvalidOperationException_WhenDatabaseOperationFails()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new InvalidOperationException());
        var controller = new ProductController(mockConnection.Object.ConnectionString);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Delete(1));
    }
}