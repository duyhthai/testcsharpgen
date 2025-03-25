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
        public async Task Get_ReturnsProductWithValidId()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open()).Verifiable();
            mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                          .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "TestProduct" } });

            var controller = new ProductController(mockConnection.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestProduct", result.Name);
            mockConnection.Verify(conn => conn.Open(), Times.Once());
        }

        [Fact]
        public async Task Get_ReturnsNullForNonExistentId()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open()).Verifiable();
            mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                          .ReturnsAsync(new List<Product>());

            var controller = new ProductController(mockConnection.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act
            var result = await controller.Get(2);

            // Assert
            Assert.Null(result);
            mockConnection.Verify(conn => conn.Open(), Times.Once());
        }

        [Fact]
        public async Task Get_ThrowsExceptionOnSqlError()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
            mockConnection.Setup(conn => conn.Open()).Verifiable();
            mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                          .ThrowsAsync(new SqlException());

            var controller = new ProductController(mockConnection.Object)
            {
                _connectionString = "FakeConnectionString"
            };

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
            mockConnection.Verify(conn => conn.Open(), Times.Once());
        }
    }
}