using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using Moq;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private string _connectionString = "MockConnectionString";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(c => c.Open());
        _mockConnection.Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ReturnsAsync(1);
    }

    [Test]
    public async Task Post_ThrowsException_WhenDatabaseOperationFails()
    {
        // Arrange
        var controller = new ProductController();
        controller.Configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        controller._connectionString = _connectionString;
        _mockConnection.Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>())).ThrowsAsync(new SqlException());

        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

        // Act & Assert
        Assert.ThrowsAsync<SqlException>(() => controller.Post(product));
    }

    [Test]
    public async Task Post_ThrowsArgumentNullException_WhenProductIsNull()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => controller.Post(null));
    }
}