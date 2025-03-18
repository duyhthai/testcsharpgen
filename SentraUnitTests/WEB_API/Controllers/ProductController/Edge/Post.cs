using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Post_ReturnsNewProductId()
    {
        // Arrange
        var mockSqlConnectionFactory = new Mock<IDbConnectionFactory>();
        var mockConnection = new Mock<SqlConnection>();
        var mockParameters = new Mock<DynamicParameters>();

        mockSqlConnectionFactory.Setup(f => f.Create()).Returns(mockConnection.Object);
        mockConnection.Setup(c => c.State).Returns(System.Data.ConnectionState.Closed);
        mockConnection.Setup(c => c.Open());
        mockConnection.Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandBehavior?>(), It.IsAny<int?>(), It.IsAny<CommandType>())).Returns(Task.FromResult(1));
        mockParameters.Setup(p => p.Get<int>(It.Is<string>(name => name == "@id"))).Returns(123);

        var controller = new ProductController(mockSqlConnectionFactory.Object)
        {
            _connectionString = "SomeConnectionString"
        };

        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.00m, IsActive = true, ImageUrl = "ImageUrl" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.Equal(123, result);
    }

    [Fact]
    public async Task Post_WithEmptySku_ThrowsArgumentException()
    {
        // Arrange
        var controller = new ProductController(null)
        {
            _connectionString = "SomeConnectionString"
        };

        var product = new Product { Sku = "", Content = "Content", Price = 10.00m, IsActive = true, ImageUrl = "ImageUrl" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Post(product));
    }

    [Fact]
    public async Task Post_WithNegativePrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var controller = new ProductController(null)
        {
            _connectionString = "SomeConnectionString"
        };

        var product = new Product { Sku = "SKU123", Content = "Content", Price = -10.00m, IsActive = true, ImageUrl = "ImageUrl" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => controller.Post(product));
    }
}