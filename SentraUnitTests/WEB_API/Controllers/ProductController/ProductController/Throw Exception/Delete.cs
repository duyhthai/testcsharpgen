using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Delete_ThrowsSqlException_WhenConnectionFails()
    {
        // Arrange
        var controller = CreateProductController();
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Delete(1));
    }

    [Fact]
    public async void Delete_ThrowsInvalidOperationException_WhenConnectionIsNotOpen()
    {
        // Arrange
        var controller = CreateProductController();
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.SetupGet(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Delete(1));
    }

    private ProductController CreateProductController()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var context = new ApplicationDbContext(options);
        return new ProductController(context)
        {
            _connectionString = "InvalidConnectionString"
        };
    }
}