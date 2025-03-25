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
        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        var id = 1;

        // Act
        await controller.Delete(id);

        // Assert
        // Assuming there's a way to verify the stored procedure was called
        // For example, by mocking the connection or using a database interceptor
    }

    [Fact]
    public async Task Delete_ShouldNotOpenConnectionIfAlreadyOpen()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        var id = 1;
        using (var mockConn = new Mock<SqlConnection>())
        {
            mockConn.Setup(c => c.State).Returns(System.Data.ConnectionState.Open);
            controller.DatabaseContext = mockConn.Object;

            // Act
            await controller.Delete(id);

            // Assert
            mockConn.Verify(c => c.Open(), Times.Never());
        }
    }

    [Fact]
    public async Task Delete_ShouldThrowArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        var id = -1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(id));
    }
}