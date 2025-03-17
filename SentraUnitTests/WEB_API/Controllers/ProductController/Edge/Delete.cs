using NUnit.Framework;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Test]
    public async Task Delete_ShouldOpenConnectionIfClosed()
    {
        // Arrange
        var controller = new ProductController();
        controller.DatabaseContext = new DatabaseContext(_connectionString);
        var mockConnection = new Mock<SqlConnection>();
        controller.DatabaseContext.Connection = mockConnection.Object;
        mockConnection.Setup(m => m.State).Returns(System.Data.ConnectionState.Closed);

        // Act
        await controller.Delete(1);

        // Assert
        mockConnection.Verify(m => m.Open(), Times.Once());
    }

    [Test]
    public async Task Delete_ShouldNotOpenConnectionIfAlreadyOpen()
    {
        // Arrange
        var controller = new ProductController();
        controller.DatabaseContext = new DatabaseContext(_connectionString);
        var mockConnection = new Mock<SqlConnection>();
        controller.DatabaseContext.Connection = mockConnection.Object;
        mockConnection.Setup(m => m.State).Returns(System.Data.ConnectionState.Open);

        // Act
        await controller.Delete(1);

        // Assert
        mockConnection.Verify(m => m.Open(), Times.Never());
    }

    [Test]
    public async Task Delete_ShouldCallExecuteAsyncWithCorrectParameters()
    {
        // Arrange
        var controller = new ProductController();
        controller.DatabaseContext = new DatabaseContext(_connectionString);
        var mockConnection = new Mock<SqlConnection>();
        controller.DatabaseContext.Connection = mockConnection.Object;
        mockConnection.Setup(m => m.State).Returns(System.Data.ConnectionState.Closed);
        var mockCommand = new Mock<SqlCommand>();
        mockConnection.Setup(m => m.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(m => m.CommandText).Returns("Delete_Product_ById");
        mockCommand.Setup(m => m.CommandType).Returns(System.Data.CommandType.StoredProcedure);

        // Act
        await controller.Delete(1);

        // Assert
        mockCommand.Verify(m => m.Parameters.AddWithValue("@id", 1), Times.Once());
        mockCommand.Verify(m => m.ExecuteNonQueryAsync(), Times.Once());
    }
}