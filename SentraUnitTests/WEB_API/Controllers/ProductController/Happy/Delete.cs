using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace WEB_API.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Delete_ShouldExecuteStoredProcedureWithValidId()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open());
            mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult(1));

            var mockConnectionFactory = new Mock<Func<string, SqlConnection>>();
            mockConnectionFactory.Setup(factory => factory(It.IsAny<string>())).Returns(mockConnection.Object);

            var productController = new ProductController(mockConnectionFactory.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act
            await productController.Delete(1);

            // Assert
            mockConnection.Verify(conn => conn.Open(), Times.Once);
            mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotOpenConnectionIfAlreadyOpen()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult(1));

            var mockConnectionFactory = new Mock<Func<string, SqlConnection>>();
            mockConnectionFactory.Setup(factory => factory(It.IsAny<string>())).Returns(mockConnection.Object);

            var productController = new ProductController(mockConnectionFactory.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act
            await productController.Delete(1);

            // Assert
            mockConnection.Verify(conn => conn.Open(), Times.Never);
            mockConnection.Verify(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContentResult()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open());
            mockConnection.Setup(conn => conn.ExecuteAsync("Delete_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).Returns(Task.FromResult(1));

            var mockConnectionFactory = new Mock<Func<string, SqlConnection>>();
            mockConnectionFactory.Setup(factory => factory(It.IsAny<string>())).Returns(mockConnection.Object);

            var productController = new ProductController(mockConnectionFactory.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act
            var result = await productController.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}