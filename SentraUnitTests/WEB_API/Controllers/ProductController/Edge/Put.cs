using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<SqlConnection> _mockConnection;
    private readonly Mock<IDataReader> _mockDataReader;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockConnection = new Mock<SqlConnection>();
        _mockDataReader = new Mock<IDataReader>();
        _mockConfig = new Mock<IConfiguration>();

        _mockConfig.SetupGet(c => c["ConnectionString"]).Returns("TestConnectionString");
        _controller = new ProductController(_mockConfig.Object);
    }

    [Fact]
    public async Task Put_WithValidProduct_ShouldUpdateProductInDatabase()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com" };
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();
        _mockConnection.Setup(conn => conn.ExecuteAsync("Update_Product", parameters, It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Update_Product", parameters, It.IsAny<CommandDefinition>()), Times.Once);
    }

    [Fact]
    public async Task Put_WithClosedConnection_ShouldOpenAndCloseConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com" };
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
        _mockConnection.Setup(conn => conn.ExecuteAsync("Update_Product", parameters, It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.Close(), Times.Once);
    }

    [Fact]
    public async Task Put_WithOpenConnection_ShouldNotReopenConnection()
    {
        // Arrange
        int id = 1;
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com" };
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);

        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockConnection.Setup(conn => conn.ExecuteAsync("Update_Product", parameters, It.IsAny<CommandDefinition>())).Returns(Task.FromResult(1));

        // Act
        await _controller.Put(id, product);

        // Assert
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
    }
}