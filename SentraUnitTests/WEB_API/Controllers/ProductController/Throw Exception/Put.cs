using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IDapper> _mockDapper;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockDapper = new Mock<IDapper>();
            _controller = new ProductController();
            _controller.Database = _mockConnection.Object;
        }

        [Fact]
        public async void Put_ThrowsException_WhenDatabaseConnectionFails()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
        }

        [Fact]
        public async void Put_ThrowsException_WhenStoredProcedureExecutionFails()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.Open());
            _mockConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandBehavior?>(), It.IsAny<int?>(), It.IsAny<CommandType>()))
                           .Throws(new SqlException());

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
        }
    }
}