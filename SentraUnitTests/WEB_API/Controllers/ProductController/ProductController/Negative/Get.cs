using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Data;
using System.Data.SqlClient;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDataReader> _mockDataReader;
    private readonly Mock<IConfiguration> _mockConfig;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDataReader = new Mock<IDataReader>();
        _mockConfig = new Mock<IConfiguration>();

        _mockConfig.Setup(c => c.GetSection("ConnectionStrings").Value).Returns("YourConnectionStringHere");
    }

    [Fact]
    public async void Get_InvalidId_ReturnsNotFound()
    {
        // Arrange
        int invalidId = -1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockConnection.Setup(conn => conn.CreateCommand()).Returns(new SqlCommand());
        _mockConnection.Object.CreateCommand().Setup(cmd => cmd.Parameters.AddWithValue("@id", invalidId)).Verifiable();

        var controller = new ProductController(_mockConfig.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Get_ConnectionClosedAndOpenThrowsException()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

        var controller = new ProductController(_mockConfig.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(validId));
    }

    [Fact]
    public async void Get_NoResultsFound_ReturnsNotFound()
    {
        // Arrange
        int validId = 1;
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockConnection.Setup(conn => conn.CreateCommand()).Returns(new SqlCommand());
        _mockConnection.Object.CreateCommand().Setup(cmd => cmd.Parameters.AddWithValue("@id", validId)).Verifiable();
        _mockConnection.Object.CreateCommand().Setup(cmd => cmd.ExecuteReader()).Returns(_mockDataReader.Object);
        _mockDataReader.Setup(dr => dr.Read()).Returns(false);

        var controller = new ProductController(_mockConfig.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get(validId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}