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
    private Mock<IDapper> _mockDapper;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _controller = new ProductController();
        _controller.DatabaseConnection = "InvalidConnectionString"; // Invalid connection string for negative scenario
    }

    [Test]
    public async Task Delete_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        int invalidId = -1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", invalidId);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _controller.Delete(invalidId));
    }

    [Test]
    public async Task Delete_WithClosedConnection_ReopensConnection()
    {
        // Arrange
        int validId = 1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", validId);
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once());
    }

    [Test]
    public async Task Delete_WithOpenConnection_DoesNotReopenConnection()
    {
        // Arrange
        int validId = 1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", validId);
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never());
    }
}