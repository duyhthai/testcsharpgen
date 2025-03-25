using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WEB_API.Models;
using Dapper;
using Xunit;

[Collection("ProductControllerTests")]
public class ProductControllerEdgeTests
{
    private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Fact]
    public async Task Get_WithEmptyConnectionString_ThrowsException()
    {
        // Arrange
        var controller = new ProductController();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get());
    }

    [Fact]
    public async Task Get_WithClosedConnection_OpensConnection()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();

        var controller = new ProductController { _connectionString = "" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        await controller.Get();

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Fact]
    public async Task Get_WithOpenConnection_DoesNotOpenConnection()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        var controller = new ProductController { _connectionString = "" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        await controller.Get();

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}