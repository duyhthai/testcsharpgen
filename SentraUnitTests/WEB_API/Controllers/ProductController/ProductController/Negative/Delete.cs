using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private ProductController _controller;
    private string _connectionString = "InvalidConnectionString";

    [SetUp]
    public void Setup()
    {
        _controller = new ProductController();
    }

    [Test]
    public async Task Delete_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        int invalidId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Delete(invalidId));
    }

    [Test]
    public async Task Delete_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 999999;

        // Mocking the database connection and command execution
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(0);

        _controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await _controller.Delete(nonExistentId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
        mockConnection.VerifyAll();
    }

    [Test]
    public async Task Delete_WithValidId_ShouldExecuteStoredProcedure()
    {
        // Arrange
        int validId = 1;

        // Mocking the database connection and command execution
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        _controller.DatabaseContext = mockConnection.Object;

        // Act
        var result = await _controller.Delete(validId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        mockConnection.VerifyAll();
    }
}