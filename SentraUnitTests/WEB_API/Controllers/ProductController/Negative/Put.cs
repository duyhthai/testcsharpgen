using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDapper> _mockDapper;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _controller = new ProductController { ConnectionString = "TestConnectionString" };
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.DapperService = _mockDapper.Object;
    }

    [Fact]
    public async Task Put_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        int id = -1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(id, product));
    }

    [Fact]
    public async Task Put_WithNullProduct_ShouldThrowArgumentNullException()
    {
        // Arrange
        int id = 1;
        Product product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Put(id, product));
    }

    [Fact]
    public async Task Put_WithEmptySku_ShouldThrowArgumentException()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(id, product));
    }
}