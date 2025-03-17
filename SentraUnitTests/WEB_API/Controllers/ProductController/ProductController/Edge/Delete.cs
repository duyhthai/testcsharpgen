using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private ProductController _controller;
    private string _connectionString;

    [SetUp]
    public void Setup()
    {
        _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        _controller = new ProductController();
    }

    [Test]
    public async Task Delete_ShouldOpenConnectionIfClosed()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(c => c.Open()).Verifiable();

        var controller = new ProductController(mockConnection.Object);

        // Act
        await controller.Delete(1);

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldExecuteStoredProcedureWithCorrectId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Open);
        mockConnection.Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1)).Verifiable();

        var controller = new ProductController(mockConnection.Object);

        // Act
        await controller.Delete(1);

        // Assert
        mockConnection.Verify(c => c.ExecuteAsync("Delete_Product_ById", It.Is<DynamicParameters>(p => p.Get<int>("@id") == 1), It.IsAny<CommandDefinition>()), Times.Once);
    }

    [Test]
    public async Task Delete_ShouldThrowExceptionIfConnectionIsNull()
    {
        // Arrange
        var controller = new ProductController(null);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => controller.Delete(1));
    }
}