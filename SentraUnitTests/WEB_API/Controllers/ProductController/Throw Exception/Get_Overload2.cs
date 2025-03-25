using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [Fact]
    public async Task Get_ThrowsExceptionOnInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        int invalidId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(invalidId));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnSqlError()
    {
        // Arrange
        var controller = new ProductController();
        int id = 1; // Assuming this ID exists in your database
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new Microsoft.AspNetCore.Routing.RouteData()
        };
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(id));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnEmptyResult()
    {
        // Arrange
        var controller = new ProductController();
        int nonExistentId = 99999; // Assuming this ID does not exist in your database
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open());
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new Microsoft.AspNetCore.Routing.RouteData()
        };
        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(nonExistentId));
    }
}