using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbCommand> _mockCommand;
    private Mock<IDataReader> _mockDataReader;
    private string _connectionString;

    [SetUp]
    public void SetUp()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockCommand = new Mock<IDbCommand>();
        _mockDataReader = new Mock<IDataReader>();
        _connectionString = "YourConnectionStringHere";
    }

    [Test]
    public async Task Post_ThrowsException_WhenDatabaseConnectionFails()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Post(product));
    }

    [Test]
    public async Task Post_ThrowsException_WhenStoredProcedureExecutionFails()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

        _mockConnection.Setup(conn => conn.CreateCommand()).Returns(_mockCommand.Object);
        _mockCommand.Setup(cmd => cmd.ExecuteNonQueryAsync()).Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Post(product));
    }
}