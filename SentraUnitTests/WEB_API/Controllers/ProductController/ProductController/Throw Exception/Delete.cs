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
    private string _connectionString = "your_connection_string_here";

    [SetUp]
    public void SetUp()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _controller = new ProductController();
        _controller.DatabaseContext = _mockConnection.Object;
    }

    [Test]
    public async Task Delete_ThrowsSqlException_WhenDatabaseOperationFails()
    {
        // Arrange
        int id = 1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);

        _mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure))
                       .Throws(new SqlException());

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Delete(id));
    }

    [Test]
    public async Task Delete_ThrowsArgumentException_WhenIdIsNegative()
    {
        // Arrange
        int id = -1;
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _controller.Delete(id));
    }

    [Test]
    public async Task Delete_ThrowsArgumentNullException_WhenConnectionStringIsNull()
    {
        // Arrange
        _controller.DatabaseContext = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Delete(1));
    }
}