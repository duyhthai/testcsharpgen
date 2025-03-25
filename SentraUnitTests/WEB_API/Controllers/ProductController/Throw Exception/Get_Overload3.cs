using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [Fact]
    public async Task Get_ThrowsSqlException_WhenDatabaseConnectionFails()
    {
        // Arrange
        var controller = new ProductController();
        var mockSqlConnection = new Mock<SqlConnection>();
        mockSqlConnection.Setup(m => m.Open()).Throws(new SqlException());
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, "Test"));
    }

    [Fact]
    public async Task Get_ThrowsInvalidOperationException_WhenNoResultsFound()
    {
        // Arrange
        var controller = new ProductController();
        var mockSqlConnection = new Mock<SqlConnection>();
        mockSqlConnection.Setup(m => m.Open());
        var mockQueryResult = new List<Product>();
        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(d => d.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(mockQueryResult.AsEnumerable()));
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        controller.Database = mockDapper.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, "Test"));
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenIdIsZero()
    {
        // Arrange
        var controller = new ProductController();
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(0, "Test"));
    }
}