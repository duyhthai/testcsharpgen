using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Post_WithInvalidProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var product = new Product { Sku = "", Content = "", Price = -1, IsActive = true, ImageUrl = "" };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Post_WithConnectionError_ReturnsInternalServerError()
    {
        // Arrange
        var mockDbConnection = new Mock<IDbConnection>();
        mockDbConnection.Setup(c => c.Open()).Throws(new SqlException());
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "image.jpg" };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.ControllerContext.HttpContext.RequestServices.GetService = (service) =>
        {
            if (service == typeof(IDbConnection))
            {
                return mockDbConnection.Object;
            }
            return null;
        };

        // Act
        var result = await controller.Post(product);

        // Assert
        Assert.IsInstanceOf<StatusCodeResult>(result);
        Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
    }
}