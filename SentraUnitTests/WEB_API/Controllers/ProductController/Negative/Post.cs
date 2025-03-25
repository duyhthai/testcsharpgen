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
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Fact]
    public async Task Post_WithNullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        var product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_WithInvalidSku_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product { Sku = "invalid_sku" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_WithNegativePrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var product = new Product { Price = -10.0m };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _controller.Post(product));
    }
}