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
        }

        [Fact]
        public async Task Put_WithException_RethrowsException()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => _controller.Put(id, product));
        }

        [Fact]
        public async Task Put_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            int id = -1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Put(id, product));
        }

        [Fact]
        public async Task Put_WithNullProduct_ThrowsArgumentNullException()
        {
            // Arrange
            int id = 1;
            Product product = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.Put(id, product));
        }
    }
}