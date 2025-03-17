using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;
    private string _connectionString = "ValidConnectionString";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.DatabaseContext = new Mock<IDatabaseContext>().Object;
        _controller.DatabaseContext.ConnectionString = _connectionString;
    }

    [Test]
    public async Task Delete_ShouldCallExecuteAsync_WithCorrectParameters()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _controller.DatabaseContext.Connection = _mockConnection.Object;

        // Act
        await _controller.Delete(id);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == id), null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldNotOpenConnection_IfAlreadyOpen()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _controller.DatabaseContext.Connection = _mockConnection.Object;

        // Act
        await _controller.Delete(id);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }
}