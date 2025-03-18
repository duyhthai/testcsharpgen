using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    [Collection("DatabaseTests")]
    public class ProductControllerTests
    {
        private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

        [Fact]
        public async Task Get_ReturnsProduct_WithValidId()
        {
            // Arrange
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext(new DefaultHttpContext(), new RouteData(), controller);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Get_ReturnsNull_WithInvalidId()
        {
            // Arrange
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext(new DefaultHttpContext(), new RouteData(), controller);

            // Act
            var result = await controller.Get(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_ThrowsException_WithEmptyConnectionString()
        {
            // Arrange
            var controller = new ProductController();
            controller.ControllerContext = new ControllerContext(new DefaultHttpContext(), new RouteData(), controller);
            controller._connectionString = "";

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await controller.Get(1));
        }
    }
}