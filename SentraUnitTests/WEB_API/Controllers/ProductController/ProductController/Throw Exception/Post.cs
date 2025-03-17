using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDbTransaction> _mockTransaction;
    private readonly Mock<IModelBinderProvider> _modelBinderProvider;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockTransaction = new Mock<IDbTransaction>();
        _modelBinderProvider = new Mock<IModelBinderProvider>();

        _controller = new ProductController();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext(),
            ModelBinderProviders = new List<IModelBinderProvider> { _modelBinderProvider.Object }
        };
    }

    [Fact]
    public async Task Post_ThrowsSqlException_WhenDatabaseOperationFails()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        _controller.ModelState.AddModelError("", "");

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_ThrowsArgumentException_WhenProductIsNull()
    {
        // Arrange
        Product product = null;
        _controller.ModelState.AddModelError("", "");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Post(product));
    }

    [Fact]
    public async Task Post_ThrowsInvalidOperationException_WhenModelStateIsInvalid()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "url" };
        _controller.ModelState.AddModelError("", "");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Post(product));
    }
}