using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c.GetConnectionString(It.IsAny<string>())).Returns("TestConnectionString");
            _controller = new ProductController(_mockConfig.Object);
        }

        [Fact]
        public async void Put_WithValidProduct_ShouldUpdateDatabase()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@sku", product.Sku);
            parameters.Add("@content", product.Content);
            parameters.Add("@price", product.Price);
            parameters.Add("@isActive", product.IsActive);
            parameters.Add("@imageUrl", product.ImageUrl);

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockConnection.Verify(conn => conn.ExecuteAsync("Update_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once());
        }

        [Fact]
        public async void Put_WithClosedConnection_ShouldOpenAndCloseConnection()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@sku", product.Sku);
            parameters.Add("@content", product.Content);
            parameters.Add("@price", product.Price);
            parameters.Add("@isActive", product.IsActive);
            parameters.Add("@imageUrl", product.ImageUrl);

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();
            _mockConnection.Setup(conn => conn.Close()).Verifiable();

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Once());
            _mockConnection.Verify(conn => conn.Close(), Times.Once());
        }

        [Fact]
        public async void Put_WithAlreadyOpenedConnection_ShouldNotReopenConnection()
        {
            // Arrange
            int id = 1;
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@sku", product.Sku);
            parameters.Add("@content", product.Content);
            parameters.Add("@price", product.Price);
            parameters.Add("@isActive", product.IsActive);
            parameters.Add("@imageUrl", product.ImageUrl);

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            _mockConnection.Setup(conn => conn.Open()).Verifiable();

            // Act
            await _controller.Put(id, product);

            // Assert
            _mockConnection.Verify(conn => conn.Open(), Times.Never());
        }
    }
}