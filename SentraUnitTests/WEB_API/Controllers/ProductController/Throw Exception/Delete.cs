using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_ShouldThrowArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "valid_connection_string" };
        var id = -1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(id));
    }

    [Fact]
    public async Task Delete_ShouldThrowArgumentNullException_WhenConnectionStringIsNull()
    {
        // Arrange
        var controller = new ProductController { _connectionString = null };
        var id = 1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Delete(id));
    }

    [Fact]
    public async Task Delete_ShouldThrowInvalidOperationException_WhenConnectionCannotBeOpened()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new InvalidOperationException());
        var controller = new ProductController { _connectionString = "invalid_connection_string" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Delete(1));
    }
}