using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "InvalidConnectionString";

    [Fact]
    public async void Get_WithInvalidConnectionString_ShouldThrowSqlException()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async void Get_WithNonExistentId_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var controller = new ProductController();
        var parameters = new DynamicParameters();
        parameters.Add("@id", int.MaxValue);

        using (var mockConnection = new MockSqlConnection())
        {
            mockConnection.SetupSequence(c => c.State)
                           .Returns(System.Data.ConnectionState.Closed)
                           .Returns(System.Data.ConnectionState.Open);
            mockConnection.Setup(c => c.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure))
                           .Throws(new InvalidOperationException("No rows found"));

            controller.DatabaseContext = mockConnection.Object;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
        }
    }

    [Fact]
    public async void Get_WithOpenConnection_ShouldReturnProduct()
    {
        // Arrange
        var controller = new ProductController();
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        var product = new Product { Id = 1, Name = "TestProduct" };

        using (var mockConnection = new MockSqlConnection())
        {
            mockConnection.SetupSequence(c => c.State)
                           .Returns(System.Data.ConnectionState.Closed)
                           .Returns(System.Data.ConnectionState.Open);
            mockConnection.Setup(c => c.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure))
                           .ReturnsAsync(new List<Product> { product });

            controller.DatabaseContext = mockConnection.Object;

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
        }
    }
}