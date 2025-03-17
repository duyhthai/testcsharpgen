using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
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
            _controller.Dapper = _mockDapper.Object;
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsNewId()
        {
            // Arrange
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var parameters = new DynamicParameters();
            parameters.Add("@sku", product.Sku);
            parameters.Add("@content", product.Content);
            parameters.Add("@price", product.Price);
            parameters.Add("@isActive", product.IsActive);
            parameters.Add("@imageUrl", product.ImageUrl);
            parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open());
            _mockDapper.Setup(dapper => dapper.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(1);
            _mockConnection.Setup(conn => conn.Parameters).Returns(parameters);

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Post_WithEmptySku_ReturnsDefaultId()
        {
            // Arrange
            var product = new Product { Sku = "", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
            var parameters = new DynamicParameters();

            _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            _mockConnection.Setup(conn => conn.Open());

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Post_WithNullProduct_ReturnsDefaultId()
        {
            // Arrange
            Product product = null;

            // Act
            var result = await _controller.Post(product);

            // Assert
            Assert.Equal(0, result);
        }
    }
}