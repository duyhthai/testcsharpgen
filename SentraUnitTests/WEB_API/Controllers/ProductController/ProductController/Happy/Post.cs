using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDapper> _mockDapper;
    private ProductController _controller;
    private string _connectionString;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _connectionString = "TestConnectionString";
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Test]
    public async Task Post_WithValidProduct_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "image.jpg" };
        var expectedResult = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.ExecuteAsync("Create_Product", It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult(1));
        _mockConnection.SetupGet(conn => conn.ConnectionString).Returns(_connectionString);

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public async Task Post_WithOpenConnection_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "image.jpg" };
        var expectedResult = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockDapper.Setup(dapper => dapper.ExecuteAsync("Create_Product", It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult(1));
        _mockConnection.SetupGet(conn => conn.ConnectionString).Returns(_connectionString);

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public async Task Post_WithException_ThrowsException()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.99m, IsActive = true, ImageUrl = "image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new Exception("Database connection failed"));
        _mockConnection.SetupGet(conn => conn.ConnectionString).Returns(_connectionString);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _controller.Post(product));
    }
}