using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private string _connectionString = "your_connection_string_here";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
    }

    [Test]
    public async Task Post_WithInvalidProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var invalidProduct = new Product { Sku = "", Content = "", Price = -1, IsActive = true, ImageUrl = "" };

        // Act & Assert
        var response = await controller.Post(invalidProduct) as BadRequestObjectResult;
        Assert.IsNotNull(response);
        Assert.AreEqual(400, response.StatusCode);
    }

    [Test]
    public async Task Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        var response = await controller.Post(null) as BadRequestObjectResult;
        Assert.IsNotNull(response);
        Assert.AreEqual(400, response.StatusCode);
    }

    [Test]
    public async Task Post_WithDatabaseError_ReturnsInternalServerError()
    {
        // Arrange
        var controller = new ProductController();
        var validProduct = new Product { Sku = "SKU123", Content = "Content", Price = 10, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        var response = await controller.Post(validProduct) as StatusCodeResult;
        Assert.IsNotNull(response);
        Assert.AreEqual(500, response.StatusCode);
    }
}