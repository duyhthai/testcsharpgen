using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        using (var mockConnection = new Mock<SqlConnection>(_connectionString))
        {
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open());
            mockConnection.Setup(conn => conn.QueryAsync<Product>("SELECT * FROM Products")).ReturnsAsync(new List<Product>());

            var controller = new ProductController(mockConnection.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Empty(result);
        }
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionStringIsNull()
    {
        // Arrange
        var controller = new ProductController(null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get());
    }

    [Fact]
    public async Task Get_ThrowsException_WhenQueryFails()
    {
        // Arrange
        using (var mockConnection = new Mock<SqlConnection>(_connectionString))
        {
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open());
            mockConnection.Setup(conn => conn.QueryAsync<Product>("SELECT * FROM Products")).ThrowsAsync(new SqlException());

            var controller = new ProductController(mockConnection.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get());
        }
    }
}