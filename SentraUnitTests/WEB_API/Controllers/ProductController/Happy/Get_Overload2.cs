using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbConnection> _mockDbConnection;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDbConnection = new Mock<IDbConnection>();
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
    }

    [Test]
    public async Task Get_ShouldReturnProduct_WhenValidIdAndNameProvided()
    {
        // Arrange
        int id = 1;
        string name = "Laptop";
        var product = new Product { Id = id, Name = name };
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                         .ReturnsAsync(new List<Product> { product });

        // Act
        var result = await _controller.Get(id, name);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(product.Id, result.Id);
        Assert.AreEqual(product.Name, result.Name);
    }

    [Test]
    public async Task Get_ShouldThrowException_WhenNoProductFound()
    {
        // Arrange
        int id = 1;
        string name = "NonExistentProduct";
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                         .ReturnsAsync(new List<Product>());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Get(id, name));
    }
}