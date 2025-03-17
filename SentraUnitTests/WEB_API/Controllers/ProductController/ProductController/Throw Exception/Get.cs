using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Get_ThrowsException_WhenConnectionIsClosed()
    {
        // Arrange
        var controller = new ProductController();
        var mockConnectionString = "InvalidConnectionString";
        controller._connectionString = mockConnectionString;

        // Act & Assert
        Assert.ThrowsAsync<SqlException>(async () => await controller.Get(1));
    }

    [Test]
    public async Task Get_ThrowsException_WhenNoProductFound()
    {
        // Arrange
        var controller = new ProductController();
        controller._connectionString = "ValidConnectionString";

        using (var mockConnection = new Mock<SqlConnection>())
        {
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                           .ReturnsAsync(new List<Product>());

            controller.ControllerContext.HttpContext.RequestServices = new ServiceCollection().AddSingleton(mockConnection.Object).BuildServiceProvider();

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await controller.Get(1));
        }
    }

    [Test]
    public async Task Get_ThrowsException_WhenQueryFails()
    {
        // Arrange
        var controller = new ProductController();
        controller._connectionString = "ValidConnectionString";

        using (var mockConnection = new Mock<SqlConnection>())
        {
            mockConnection.Setup(conn => conn.State).Returns(System.Data.ConnectionState.Open);
            mockConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure))
                           .ThrowsAsync(new SqlException());

            controller.ControllerContext.HttpContext.RequestServices = new ServiceCollection().AddSingleton(mockConnection.Object).BuildServiceProvider();

            // Act & Assert
            Assert.ThrowsAsync<SqlException>(async () => await controller.Get(1));
        }
    }
}