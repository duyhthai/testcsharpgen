using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDataReader> _mockDataReader;
    private readonly Mock<ICommandDefinition> _mockCommandDefinition;
    private readonly string _connectionString = "TestConnectionString";

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDataReader = new Mock<IDataReader>();
        _mockCommandDefinition = new Mock<ICommandDefinition>();

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open());
        _mockConnection.Setup(conn => conn.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
            .ReturnsAsync((string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType commandType) =>
            {
                _mockDataReader.Setup(dr => dr.Read()).Returns(false);
                return Task.FromResult<IEnumerable<Product>>(new List<Product>());
            });
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var controller = new ProductController { RequestServices = new ServiceCollection().AddSingleton<SqlConnection>(_mockConnection.Object).BuildServiceProvider() };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenConnectionFailsToOpen()
    {
        // Arrange
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException("Failed to open connection"));

        var controller = new ProductController { RequestServices = new ServiceCollection().AddSingleton<SqlConnection>(_mockConnection.Object).BuildServiceProvider() };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsException_WhenInvalidIdProvided()
    {
        // Arrange
        var controller = new ProductController { RequestServices = new ServiceCollection().AddSingleton<SqlConnection>(_mockConnection.Object).BuildServiceProvider() };
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(-1));
    }
}