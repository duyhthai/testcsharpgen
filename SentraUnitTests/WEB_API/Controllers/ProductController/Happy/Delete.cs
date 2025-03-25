using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;
using System.Data.SqlClient;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_ShouldExecuteStoredProcedureWithValidId()
    {
        // Arrange
        int id = 1;
        string connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = connectionString
        };

        // Act
        await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == id), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Delete_ShouldNotOpenConnectionIfAlreadyOpen()
    {
        // Arrange
        int id = 1;
        string connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = connectionString
        };

        // Act
        await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Never());
        mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == id), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContentOnSuccess()
    {
        // Arrange
        int id = 1;
        string connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = connectionString
        };

        // Act
        IActionResult result = await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == id), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
        Assert.IsType<NoContentResult>(result);
    }
}