using Xunit;
using WEB_API.Controllers;
using WEB_API.Models;
using Moq;
using System.Threading.Tasks;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsProductWithCorrectId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);

        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", parameters)).ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test Product" } });

        var controller = new ProductController(mockDapper.Object)
        {
            _connectionString = "mock_connection_string"
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Product", result.Name);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
    }

    [Fact]
    public async Task Get_ReturnsNullForNonExistentProductId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();

        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);

        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", parameters)).ReturnsAsync(new List<Product>());

        var controller = new ProductController(mockDapper.Object)
        {
            _connectionString = "mock_connection_string"
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.Null(result);
        mockConnection.Verify(conn => conn.Open(), Times.Once());
    }
}