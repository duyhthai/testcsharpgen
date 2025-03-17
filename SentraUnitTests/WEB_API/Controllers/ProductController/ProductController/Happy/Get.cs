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
                          .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test Product" } });

            var productController = new ProductController(mockConnection.Object) { _connectionString = "MockConnectionString" };

            // Act
            var result = await productController.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Product", result.Name);
            mockConnection.Verify(conn => conn.Open(), Times.Once());
            mockConnection.Verify(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
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

            var productController = new ProductController(mockConnection.Object) { _connectionString = "MockConnectionString" };

            // Act
            var result = await productController.Get(1);

            // Assert
            Assert.Null(result);
            mockConnection.Verify(conn => conn.Open(), Times.Once());
            mockConnection.Verify(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
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

            var productController = new ProductController(mockConnection.Object) { _connectionString = "MockConnectionString" };

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => productController.Get(1));
            mockConnection.Verify(conn => conn.Open(), Times.Once());
            mockConnection.Verify(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure), Times.Once());
        }
    }
}