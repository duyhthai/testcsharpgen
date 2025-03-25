using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WEB_API.Models;
using Dapper;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsExceptionOnInvalidId()
    {
        // Arrange
        var controller = new ProductController();
        var invalidId = -1; // Invalid ID

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(invalidId));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnSqlError()
    {
        // Arrange
        var controller = new ProductController();
        var mockConnectionString = "Server=nonexistentserver;Database=nonexistentdb;User Id=nonexistentuser;Password=nonexistentpassword;";
        var controllerField = typeof(ProductController).GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        controllerField.SetValue(controller, mockConnectionString);

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsExceptionOnEmptyResult()
    {
        // Arrange
        var controller = new ProductController();
        var emptyResultConnectionString = "Server=localhost;Database=emptyresultdb;User Id=sa;Password=yourStrong(!)Passw0rd;";
        var controllerField = typeof(ProductController).GetField("_connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        controllerField.SetValue(controller, emptyResultConnectionString);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
    }
}