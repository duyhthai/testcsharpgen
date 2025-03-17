using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async void Post_WithInvalidProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();
        var invalidProduct = new Product { Sku = "", Content = "", Price = -1, IsActive = true, ImageUrl = "" };

        // Act & Assert
        var result = await controller.Post(invalidProduct);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Post_WithNullProduct_ReturnsBadRequest()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        var result = await controller.Post(null);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Post_WithEmptyConnectionString_ReturnsInternalServerError()
    {
        // Arrange
        var controller = new ProductController();
        controller._connectionString = string.Empty;

        // Act & Assert
        var result = await controller.Post(new Product());
        Assert.IsType<StatusCodeResult>(result);
        var statusCodeResult = result as StatusCodeResult;
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}