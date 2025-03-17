using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Data.SqlClient;
using Dapper;

[TestFixture]
public class ProductControllerTests
{
    private string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _controller = new ProductController();
        _controller._connectionString = _connectionString;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up database state if necessary
    }

    [Test]
    public async Task Post_WithValidProduct_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "image.jpg" };

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.IsNotEmpty(result);
    }

    [Test]
    public async Task Post_WithEmptySku_ReturnsError()
    {
        // Arrange
        var product = new Product { Sku = "", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "image.jpg" };

        // Act & Assert
        var ex = Assert.ThrowsAsync<SqlException>(async () => await _controller.Post(product));
        Assert.AreEqual(22001, ex.HResult); // SQL Server error code for 'string or binary data would be truncated'
    }

    [Test]
    public async Task Post_WithMaxPrice_ReturnsSuccess()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = decimal.MaxValue, IsActive = true, ImageUrl = "image.jpg" };

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.IsNotEmpty(result);
    }
}