using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var controller = new ProductController();
        int negativeId = -1;
        string validName = "TestProduct";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(negativeId, validName));
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenNameIsNull()
    {
        // Arrange
        var controller = new ProductController();
        int validId = 1;
        string nullName = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(validId, nullName));
    }

    [Fact]
    public async Task Get_ThrowsArgumentException_WhenNameIsEmpty()
    {
        // Arrange
        var controller = new ProductController();
        int validId = 1;
        string emptyName = "";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => controller.Get(validId, emptyName));
    }
}