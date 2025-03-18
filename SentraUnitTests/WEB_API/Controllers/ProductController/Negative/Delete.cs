using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Delete_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var controller = new ProductController();
            int invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.Delete(invalidId));
        }

        [Fact]
        public async Task Delete_NullConnectionString_ThrowsArgumentNullException()
        {
            // Arrange
            var controller = new ProductController(null);
            int validId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Delete(validId));
        }

        [Fact]
        public async Task Delete_ConnectionFailed_ThrowsSqlException()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
            var controller = new ProductController(mockConnection.Object);
            int validId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Delete(validId));
        }
    }
}