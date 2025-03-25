using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Get_ThrowsSqlException_WhenDatabaseConnectionFails()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("InvalidConnectionString");

            var controller = new ProductController(mockConfiguration.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get());
        }

        [Fact]
        public async Task GetById_ThrowsSqlException_WhenDatabaseConnectionFails()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("InvalidConnectionString");

            var controller = new ProductController(mockConfiguration.Object);

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
        }

        [Fact]
        public async Task Post_ThrowsSqlException_WhenDatabaseConnectionFails()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("InvalidConnectionString");

            var controller = new ProductController(mockConfiguration.Object);
            var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Post(product));
        }
    }
}