using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using Moq;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController { _connectionString = "TestConnectionString" };
    }

    [Test]
    public async Task Delete_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        int invalidId = -1;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _controller.Delete(invalidId));
    }

    [Test]
    public async Task Delete_WithClosedConnection_OpensConnectionBeforeExecution()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_WithOpenConnection_DoesNotOpenConnectionAgain()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}