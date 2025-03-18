using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _controller = new ProductController();
            _controller.DatabaseContext = _mockConnection.Object;
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoDataFound()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                           .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ThrowsException_WhenConnectionIsClosed()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get());
        }

        [Fact]
        public async Task Get_ThrowsException_WhenCommandTextIsEmpty()
        {
            // Arrange
            _mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                           .Throws(new InvalidOperationException("Invalid command text."));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get());
        }
    }
}