using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    [Collection("ProductControllerTests")]
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IDbCommand> _mockCommand;
        private readonly Mock<IDataReader> _mockDataReader;
        private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockCommand = new Mock<IDbCommand>();
            _mockDataReader = new Mock<IDataReader>();

            _mockConnection.Setup(conn => conn.CreateCommand()).Returns(_mockCommand.Object);
            _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockDataReader.Object);

            _controller = new ProductController();
        }

        [Fact]
        public async Task Get_ReturnsProductWithValidId()
        {
            // Arrange
            int validId = 1;
            var product = new Product { Id = validId, Name = "Test Product" };
            _mockDataReader.Setup(reader => reader.Read()).Returns(true).Verifiable();
            _mockDataReader.SetupSequence(reader => reader["Id"]).Returns(product.Id);
            _mockDataReader.SetupSequence(reader => reader["Name"]).Returns(product.Name);

            // Act
            var result = await _controller.Get(validId);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@id", validId), Times.Once());
            _mockDataReader.Verify(reader => reader.Read(), Times.Once());
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task Get_ReturnsNullForInvalidId()
        {
            // Arrange
            int invalidId = -1;
            _mockDataReader.Setup(reader => reader.Read()).Returns(false).Verifiable();

            // Act
            var result = await _controller.Get(invalidId);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@id", invalidId), Times.Once());
            _mockDataReader.Verify(reader => reader.Read(), Times.Once());
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_ThrowsExceptionForClosedConnection()
        {
            // Arrange
            int validId = 1;
            var product = new Product { Id = validId, Name = "Test Product" };
            _mockDataReader.Setup(reader => reader.Read()).Returns(true).Verifiable();
            _mockDataReader.SetupSequence(reader => reader["Id"]).Returns(product.Id);
            _mockDataReader.SetupSequence(reader => reader["Name"]).Returns(product.Name);
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get(validId));
        }
    }
}