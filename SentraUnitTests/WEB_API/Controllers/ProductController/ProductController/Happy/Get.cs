using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsProduct_WhenIdIsValid()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        var mockCommand = new Mock<IDbCommand>();
        var mockDataReader = new Mock<IDataReader>();
        var mockDbParameter = new Mock<IDbDataParameter>();

        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(cmd => cmd.Parameters.Add("@id")).Returns(mockDbParameter.Object);
        mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockDataReader.Object);

        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once());
        mockCommand.Verify(cmd => cmd.CommandText, Times.Once(), x => x.Equals("Get_Product_ById"));
        mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Never());
        mockDataReader.Verify(dr => dr.Read(), Times.Once());
        mockDataReader.Verify(dr => dr[0], Times.Once());
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Get_ReturnsNull_WhenNoProductFound()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        var mockCommand = new Mock<IDbCommand>();
        var mockDataReader = new Mock<IDataReader>();

        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockDataReader.Object);
        mockDataReader.Setup(dr => dr.Read()).Returns(false);

        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once());
        mockCommand.Verify(cmd => cmd.CommandText, Times.Once(), x => x.Equals("Get_Product_ById"));
        mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Never());
        mockDataReader.Verify(dr => dr.Read(), Times.Once());
        mockDataReader.Verify(dr => dr[0], Times.Never());
        Assert.Null(result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenDatabaseErrorOccurs()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        var mockCommand = new Mock<IDbCommand>();

        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(cmd => cmd.ExecuteReader()).Throws(new SqlException());

        var controller = new ProductController { _connectionString = "ValidConnectionString" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(async () => await controller.Get(1));
    }
}