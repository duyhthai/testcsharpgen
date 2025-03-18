using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbConnection> _mockDbConnection;
    private string _connectionString = "TestConnectionString";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDbConnection = new Mock<IDbConnection>();

        // Arrange
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.As<IDbConnection>().Setup(dbConn => dbConn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));
    }

    [Test]
    public async Task Put_UpdatesProductSuccessfully()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        controller.ModelState.Clear();

        // Act
        var result = await controller.Put(1, product);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.As<IDbConnection>().Verify(dbConn => dbConn.ExecuteAsync("Update_Product", It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Once);
    }

    [Test]
    public async Task Put_ReturnsNotFoundIfProductDoesNotExist()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        controller.ModelState.Clear();

        _mockConnection.As<IDbConnection>().Setup(dbConn => dbConn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(0));

        // Act
        var result = await controller.Put(1, product);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.As<IDbConnection>().Verify(dbConn => dbConn.ExecuteAsync("Update_Product", It.IsAny<object>(), It.IsAny<CommandDefinition>()), Times.Once);
    }
}