using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Fact]
    public async Task Get_ThrowsExceptionOnInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        var invalidId = int.MinValue;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(invalidId));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnSqlError()
    {
        // Arrange
        var controller = new ProductController();
        var nonExistentId = 999999;

        // Mocking SQL error
        using (var mockConnection = new Mock<SqlConnection>(_connectionString))
        {
            mockConnection.Setup(m => m.Open()).Throws(new SqlException());
            controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get(nonExistentId));
        }
    }

    [Fact]
    public async Task Get_ReturnsNullForNonExistentId()
    {
        // Arrange
        var controller = new ProductController();
        var nonExistentId = 999999;

        // Mocking empty result
        using (var mockConnection = new Mock<SqlConnection>(_connectionString))
        {
            mockConnection.Setup(m => m.Open());
            controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);

            mockConnection.Setup(m => m.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                           .ReturnsAsync((IEnumerable<Product>)null);

            // Act
            var result = await controller.Get(nonExistentId);

            // Assert
            Assert.Null(result);
        }
    }
}