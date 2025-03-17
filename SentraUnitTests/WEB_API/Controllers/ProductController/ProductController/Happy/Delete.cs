using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Delete_WithValidId_CallsExecuteAsync()
    {
        // Arrange
        int id = 1;
        string connectionString = "YourConnectionStringHere";
        var mockConnection = new Mock<SqlConnection>(connectionString);
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object) { _connectionString = connectionString };

        // Act
        await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once);
        mockConnection.Verify(conn => conn.ExecuteAsync(It.Is<string>(s => s == "Delete_Product_ById"), It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Once);
    }

    [Test]
    public async Task Delete_WithClosedConnection_OpensConnectionBeforeExecuting()
    {
        // Arrange
        int id = 1;
        string connectionString = "YourConnectionStringHere";
        var mockConnection = new Mock<SqlConnection>(connectionString);
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object) { _connectionString = connectionString };

        // Act
        await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_WithOpenConnection_DoesNotOpenConnectionAgain()
    {
        // Arrange
        int id = 1;
        string connectionString = "YourConnectionStringHere";
        var mockConnection = new Mock<SqlConnection>(connectionString);
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).ReturnsAsync(1);

        var controller = new ProductController(mockConnection.Object) { _connectionString = connectionString };

        // Act
        await controller.Delete(id);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}