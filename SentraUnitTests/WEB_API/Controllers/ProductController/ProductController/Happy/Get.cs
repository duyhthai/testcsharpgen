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

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();

        _controller = new ProductController();
        _controller.Database = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Test]
    public async Task Get_ReturnsProductWithValidId()
    {
        // Arrange
        int id = 1;
        var product = new Product { Id = id, Name = "Test Product" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                   .ReturnsAsync(new List<Product> { product });

        // Act
        var result = await _controller.Get(id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual("Test Product", result.Name);
    }

    [Test]
    public async Task Get_ReturnsNullForNonExistentId()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                   .ReturnsAsync(new List<Product>());

        // Act
        var result = await _controller.Get(id);

        // Assert
        Assert.IsNull(result);
    }
}