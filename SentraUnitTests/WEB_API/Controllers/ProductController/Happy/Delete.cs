using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;

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
    public async Task Delete_ShouldCallExecuteAsync_WithCorrectParameters()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockDbConnection.Setup(dbConn => dbConn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition?>())).Returns(Task.FromResult(1));

        // Act
        await _controller.Delete(id);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockDbConnection.Verify(dbConn => dbConn.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == id), null, null, CommandType.StoredProcedure), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldNotThrowException_WithValidInput()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockDbConnection.Setup(dbConn => dbConn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition?>())).Returns(Task.FromResult(1));

        // Act & Assert
        await Assert.DoesNotThrowAsync(() => _controller.Delete(id));
    }

    [Test]
    public async Task Delete_ShouldReturnNoContentResult_WithSuccessfulDeletion()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockDbConnection.Setup(dbConn => dbConn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition?>())).Returns(Task.FromResult(1));

        // Act
        IActionResult result = await _controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}