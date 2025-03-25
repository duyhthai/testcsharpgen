using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using WEB_API.Controllers;
using WEB_API.Models;
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
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task Post_WithClosedConnection_ThrowsSqlException()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _controller.DatabaseContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer("").Options);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => _controller.Post(product));
        }

        [Fact]
        public async Task Post_WithInvalidConnectionString_ThrowsArgumentException()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _controller.DatabaseContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer("").Options);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Post(product));
        }

        [Fact]
        public async Task Post_WithNullProduct_ThrowsArgumentNullException()
        {
            // Arrange
            Product product = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Post(product));
        }
    }
}