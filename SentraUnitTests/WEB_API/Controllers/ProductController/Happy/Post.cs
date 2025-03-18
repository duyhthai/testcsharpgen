using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbCommand> _mockDbCommand;
    private Mock<IDataReader> _mockDataReader;
    private string _connectionString = "TestConnectionString";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _mockDbCommand = new Mock<IDbCommand>();
        _mockDataReader = new Mock<IDataReader>();

        _mockConnection.Setup(conn => conn.CreateCommand()).Returns(_mockDbCommand.Object);
        _mockDbCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockDataReader.Object);
        _mockDbCommand.Setup(cmd => cmd.Parameters).Returns(new DynamicParameters());
    }

    [Test]
    public async Task Post_ShouldReturnNewProductId_WithValidProduct()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com" };
        _mockDbCommand.Setup(cmd => cmd.ExecuteNonQueryAsync()).ReturnsAsync(1);

        // Act
        var result = await controller.Post(product) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOf<int>(result.Value);
        Assert.AreEqual(1, result.Value);
    }

    [Test]
    public async Task Post_ShouldThrowException_WithInvalidProduct()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "", Content = "", Price = -1, IsActive = false, ImageUrl = "" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Post(product));
    }
}