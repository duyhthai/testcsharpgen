using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using Dapper;
using Moq;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
    }

    [Test]
    public async Task Delete_ShouldCallExecuteAsync_WithCorrectParameters()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

        // Act
        await _controller.Delete(id);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync(
            "Delete_Product_ById",
            It.Is<DynamicParameters>(p => p.Get<int>("@id") == id),
            null,
            null,
            System.Data.CommandType.StoredProcedure
        ), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldNotOpenConnection_IfAlreadyOpen()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await _controller.Delete(id);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
        _mockConnection.Verify(conn => conn.ExecuteAsync(
            "Delete_Product_ById",
            It.IsAny<DynamicParameters>(),
            null,
            null,
            System.Data.CommandType.StoredProcedure
        ), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldThrowException_IfConnectionThrows()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        Assert.ThrowsAsync<SqlException>(() => _controller.Delete(id));
    }
}