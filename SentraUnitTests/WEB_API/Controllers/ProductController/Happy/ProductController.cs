using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ReturnsListOfProducts()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.QueryAsync<Product>("", null, null, null, System.Data.CommandType.Text))
                      .ReturnsAsync(new List<Product>());

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get();

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetById_ReturnsProduct()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        var product = new Product { Id = 1, Sku = "SKU123" };
        mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(new List<Product> { product });

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.Get(1);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }

    [Fact]
    public async Task Post_CreatesNewProductAndReturnsId()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        mockConnection.Setup(conn => conn.ExecuteAsync("Create_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                      .ReturnsAsync(1);

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config.GetConnectionString("DbConnectionString")).Returns("TestConnectionString");

        var controller = new ProductController(mockConfiguration.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "ImageUrl" };

        // Act
        var result = await controller.Post(product);

        // Assert
        mockConnection.Verify(conn => conn.Open(), Times.Once());
        Assert.Equal(1, result);
    }
}