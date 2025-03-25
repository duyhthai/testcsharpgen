using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsProductWithValidId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();

        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "TestProduct" } });

        var controller = new ProductController(mockConnection.Object, mockDapper.Object);

        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("TestProduct", result.Name);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockDapper.Verify(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Get_ReturnsNullForNonExistentId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();

        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(new List<Product>());

        var controller = new ProductController(mockConnection.Object, mockDapper.Object);

        // Act
        var result = await controller.Get(2);

        // Assert
        Assert.Null(result);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        mockDapper.Verify(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
    }

    [Fact]
    public async Task Get_ThrowsExceptionForInvalidConnectionString()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        var mockDapper = new Mock<IDapper>();

        var controller = new ProductController(mockConnection.Object, mockDapper.Object);

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }
}