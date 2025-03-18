using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>();
        _controller = new ProductController();
        _controller.DatabaseContext = new Mock<IHttpContextAccessor>().Object; // Assuming HttpContextAccessor is used
    }

    [Test]
    public async Task Post_WithValidProduct_ReturnsNewId()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.0m, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        var parameters = new DynamicParameters();
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);
        parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        _mockConnection.Setup(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure))
                       .Returns(Task.FromResult(1));

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.AreEqual(1, result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Test]
    public async Task Post_WithClosedConnection_OpensConnection()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.0m, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Closed);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        var parameters = new DynamicParameters();
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);
        parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        _mockConnection.Setup(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure))
                       .Returns(Task.FromResult(1));

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.AreEqual(1, result);
        _mockConnection.Verify(conn => conn.Open(), Times.Once);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }

    [Test]
    public async Task Post_WithOpenConnection_DoesNotOpenConnection()
    {
        // Arrange
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 10.0m, IsActive = true, ImageUrl = "url" };
        _mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
        _mockConnection.Setup(conn => conn.Open()).Verifiable();

        var parameters = new DynamicParameters();
        parameters.Add("@sku", product.Sku);
        parameters.Add("@content", product.Content);
        parameters.Add("@price", product.Price);
        parameters.Add("@isActive", product.IsActive);
        parameters.Add("@imageUrl", product.ImageUrl);
        parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        _mockConnection.Setup(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure))
                       .Returns(Task.FromResult(1));

        // Act
        var result = await _controller.Post(product);

        // Assert
        Assert.AreEqual(1, result);
        _mockConnection.Verify(conn => conn.Open(), Times.Never);
        _mockConnection.Verify(conn => conn.ExecuteAsync("Create_Product", parameters, null, null, System.Data.CommandType.StoredProcedure), Times.Once);
    }
}