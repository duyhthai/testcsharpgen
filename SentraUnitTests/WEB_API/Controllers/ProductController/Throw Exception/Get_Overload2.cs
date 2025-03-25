using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsExceptionOnSqlError()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        var controller = new ProductController { _connectionString = "TestConnectionString" };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnInvalidId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open());
        var controller = new ProductController { _connectionString = "TestConnectionString" };
        var parameters = new DynamicParameters();
        parameters.Add("@id", -1);
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult<IEnumerable<Product>>(null));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(-1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnEmptyResult()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open());
        var controller = new ProductController { _connectionString = "TestConnectionString" };
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
    }
}