using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Fact]
    public async Task Get_ReturnsProducts()
    {
        // Arrange
        var controller = new ProductController();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Mocking the connection and query result
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product> { new Product { Id = 1, Name = "Product1" } }));

        controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result.First().Id);
        Assert.Equal("Product1", result.First().Name);
    }

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenNoProductsFound()
    {
        // Arrange
        var controller = new ProductController();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Mocking the connection and query result
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}