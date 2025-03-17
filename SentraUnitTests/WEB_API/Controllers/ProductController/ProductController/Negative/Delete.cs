using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDapper> _mockDapper;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDapper = new Mock<IDapper>();
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
        _controller.Dapper = _mockDapper.Object;
    }

    [Fact]
    public async Task Delete_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        int invalidId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Delete(invalidId));
    }

    [Fact]
    public async Task Delete_WithClosedConnection_ShouldOpenConnectionBeforeExecution()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        await _controller.Delete(validId);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once());
    }

    [Fact]
    public async Task Delete_WithNonExistentProduct_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 100;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                   .ReturnsAsync(0); // Assuming 0 means no rows affected

        // Act
        var result = await _controller.Delete(nonExistentId) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }
}