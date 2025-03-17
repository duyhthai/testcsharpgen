using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsEmptyArray_WhenNoData()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as IActionResult;

        // Assert
        var contentResult = Assert.IsType<ObjectResult>(result);
        Assert.Empty(contentResult.Value as IEnumerable<WeatherForecast>);
    }

    [Fact]
    public void Get_ThrowsArgumentException_WhenInputIsNull()
    {
        // Arrange
        var controller = new WeatherForecastController();
        controller.ModelState.AddModelError("error", "Invalid input");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => controller.Get());
    }

    [Fact]
    public void Get_ThrowsInvalidOperationException_WhenDatabaseConnectionFails()
    {
        // Arrange
        var controller = new WeatherForecastController();
        controller.ModelState.AddModelError("error", "Database connection failed");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => controller.Get());
    }
}