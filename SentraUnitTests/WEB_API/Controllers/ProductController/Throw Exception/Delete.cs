using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [Fact]
    public async Task Delete_ThrowsSqlException_WhenDatabaseOperationFails()
    {
        // Arrange
        int id = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        _controller.ProductRepository = new ProductRepository(_mockConnection.Object);

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => _controller.Delete(id));
    }

    [Fact]
    public async Task Delete_ThrowsArgumentNullException_WhenIdIsNull()
    {
        // Arrange
        int? id = null;
        _controller.ProductRepository = new ProductRepository(_mockConnection.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Delete(id.Value));
    }
}