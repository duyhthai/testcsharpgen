using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_ShouldThrowArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController();
        int negativeId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(negativeId));
    }

    [Fact]
    public async Task Delete_ShouldThrowArgumentNullException_WhenConnectionStringIsNull()
    {
        // Arrange
        var controller = new ProductController(null);
        int validId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Delete(validId));
    }

    [Fact]
    public async Task Delete_ShouldThrowInvalidOperationException_WhenDatabaseOperationFails()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Throws(new InvalidOperationException());
        var controller = new ProductController(mockConnection.Object);
        int validId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Delete(validId));
    }
}