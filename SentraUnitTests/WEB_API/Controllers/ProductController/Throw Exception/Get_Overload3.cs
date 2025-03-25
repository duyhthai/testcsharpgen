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
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        parameters.Add("@name", "Test");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, "Test"));
    }

    [Fact]
    public async void Get_ThrowsException_WithNonExistentStoredProcedure()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        parameters.Add("@name", "NonExistentProcedure");

        // Mocking the connection to throw an exception when executing the stored procedure
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
                       .Throws(new SqlException("No such object"));

        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, "NonExistentProcedure"));
    }

    [Fact]
    public async void Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        var parameters = new DynamicParameters();
        parameters.Add("@id", -1);
        parameters.Add("@name", "Test");

        // Mocking the connection to throw an exception when executing the stored procedure
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
                       .Throws(new InvalidOperationException("Invalid ID"));

        controller.DatabaseContext = mockConnection.Object;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(-1, "Test"));
    }
}