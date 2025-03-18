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
    private ProductController _controller;
    private string _connectionString = "YourConnectionStringHere";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.DatabaseContext = new DatabaseContext { ConnectionString = _connectionString };
    }

    [Test]
    public async Task Delete_WithValidId_ShouldNotThrowException()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _controller.DatabaseContext.Connection = _mockConnection.Object;

        // Act & Assert
        await _controller.Delete(validId);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_WithClosedConnection_ShouldOpenConnection()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _controller.DatabaseContext.Connection = _mockConnection.Object;

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_WithOpenConnection_ShouldNotOpenConnection()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _controller.DatabaseContext.Connection = _mockConnection.Object;

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}