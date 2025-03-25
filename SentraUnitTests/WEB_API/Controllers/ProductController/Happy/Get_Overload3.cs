using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsProduct_WithValidIdAndName()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "TestProduct" } });

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "FakeConnectionString"
        };

        // Act
        var result = await controller.Get(1, "TestProduct");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("TestProduct", result.Name);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Get_ReturnsNull_WithInvalidId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product>());

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "FakeConnectionString"
        };

        // Act
        var result = await controller.Get(2, "NonExistentProduct");

        // Assert
        Assert.Null(result);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Get_ReturnsNull_WithEmptyName()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product>());

        var controller = new ProductController(mockConnection.Object)
        {
            _connectionString = "FakeConnectionString"
        };

        // Act
        var result = await controller.Get(1, "");

        // Assert
        Assert.Null(result);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockConnection.Verify(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }
}