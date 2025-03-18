using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerNegativeTests
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
    public async Task Get_InvalidId_ReturnsNotFound()
    {
        // Arrange
        int id = -1;
        string name = "test";
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure))
                          .Returns(Task.FromResult<IEnumerable<Product>>(null));

        // Act
        var result = await _controller.Get(id, name);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }

    [Test]
    public async Task Get_InvalidName_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        string name = null;
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure))
                          .Returns(Task.FromResult<IEnumerable<Product>>(null));

        // Act
        var result = await _controller.Get(id, name);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }

    [Test]
    public async Task Get_EmptyResult_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        string name = "test";
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure))
                          .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act
        var result = await _controller.Get(id, name);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }
}