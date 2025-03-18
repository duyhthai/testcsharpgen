using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsProductForValidId()
    {
        // Arrange
        int validId = 1;
        var product = new Product { Id = validId, Name = "Test Product" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product> { product });

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "SomeConnectionString"
        };

        // Act
        var result = await controller.Get(validId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(validId, result.Id);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public async Task Get_ReturnsNullForInvalidId()
    {
        // Arrange
        int invalidId = -1;
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open());
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product>());

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "SomeConnectionString"
        };

        // Act
        var result = await controller.Get(invalidId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Get_ThrowsExceptionForClosedConnection()
    {
        // Arrange
        int validId = 1;
        var product = new Product { Id = validId, Name = "Test Product" };
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "SomeConnectionString"
        };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(validId));
    }
}