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
        _controller = new ProductController();
        _connectionString = "ValidConnectionString";
        _controller.DatabaseContext = new DatabaseContext { ConnectionString = _connectionString };
    }

    [Test]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        int invalidId = -1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<dynamic>(), null, null, System.Data.CommandType.StoredProcedure))
                   .Throws(new SqlException());

        // Act
        var result = await _controller.Get(invalidId);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }

    [Test]
    public async Task Get_WithNonExistentProduct_ReturnsNotFound()
    {
        // Arrange
        int nonExistentId = 999;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockDapper.Setup(dapper => dapper.QueryAsync<Product>("Get_Product_ById", It.IsAny<dynamic>(), null, null, System.Data.CommandType.StoredProcedure))
                   .ReturnsAsync((IEnumerable<Product>)null);

        // Act
        var result = await _controller.Get(nonExistentId);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }

    [Test]
    public async Task Get_WithEmptyConnectionString_ThrowsArgumentException()
    {
        // Arrange
        _connectionString = "";
        _controller.DatabaseContext = new DatabaseContext { ConnectionString = _connectionString };

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _controller.Get(1));
    }
}