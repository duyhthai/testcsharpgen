using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [Fact]
    public async Task Get_ReturnsProduct_WithValidIdAndName()
    {
        // Arrange
        var controller = new ProductController();
        var id = 1;
        var name = "TestProduct";
        var mockResult = new Product { Id = id, Name = name };
        Mock<SqlConnection>.AutoMocker.GetMock<SqlConnection>().Setup(m => m.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(new List<Product> { mockResult });

        // Act
        var result = await controller.Get(id, name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController();
        var id = -1;
        var name = "TestProduct";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(id, name));
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenNameIsNull()
    {
        // Arrange
        var controller = new ProductController();
        var id = 1;
        var name = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(id, name));
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenNameIsEmpty()
    {
        // Arrange
        var controller = new ProductController();
        var id = 1;
        var name = "";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(id, name));
    }
}