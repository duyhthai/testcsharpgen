using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using Moq;
using System.Data.SqlClient;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;
    private string _connectionString = "YourConnectionStringHere";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _controller = new ProductController();
    }

    [Test]
    public async Task Get_WithInvalidId_ReturnsNull()
    {
        // Arrange
        int invalidId = -1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        // Act
        var result = await _controller.Get(invalidId);

        // Assert
        Assert.IsNull(result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once());
    }

    [Test]
    public async Task Get_WithNonExistentId_ReturnsNull()
    {
        // Arrange
        int nonExistentId = 999;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
            .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act
        var result = await _controller.Get(nonExistentId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task Get_WithOpenConnectionAndNoResults_ReturnsNull()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
            .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act
        var result = await _controller.Get(id);

        // Assert
        Assert.IsNull(result);
    }
}