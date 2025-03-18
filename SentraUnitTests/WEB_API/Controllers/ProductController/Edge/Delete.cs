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
    private string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _mockDbConnection = new Mock<IDbConnection>();
        _mockDbConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockDbConnection.Setup(conn => conn.Open()).Verifiable();
        _mockDbConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition?>(), It.IsAny<CancellationToken?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);

        var factoryMock = new Mock<Func<IDbConnection>>();
        factoryMock.Setup(factory => factory()).Returns(() => _mockDbConnection.Object);

        var controller = new ProductController(factoryMock.Object);
    }

    [Test]
    public async Task Delete_ShouldOpenConnectionIfClosed()
    {
        // Arrange
        var controller = new ProductController(new Mock<Func<IDbConnection>>().Object);
        int id = 1;

        // Act
        await controller.Delete(id);

        // Assert
        _mockDbConnection.Verify(conn => conn.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldExecuteStoredProcedureWithCorrectParameters()
    {
        // Arrange
        var controller = new ProductController(new Mock<Func<IDbConnection>>().Object);
        int id = 1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);

        // Act
        await controller.Delete(id);

        // Assert
        _mockDbConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldReturnNoContentResult()
    {
        // Arrange
        var controller = new ProductController(new Mock<Func<IDbConnection>>().Object);
        int id = 1;

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}