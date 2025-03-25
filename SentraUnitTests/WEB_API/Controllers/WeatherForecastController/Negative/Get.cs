using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_WithInvalidDataType_ReturnsBadRequest()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act & Assert
        var result = Assert.ThrowsAsync<BadRequestObjectResult>(() => controller.Get());
    }

    [Fact]
    public void Get_WithNullSummariesArray_ReturnsInternalServerError()
    {
        // Arrange
        var controller = new WeatherForecastController();
        typeof(WeatherForecastController).GetProperty("Summaries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(controller, null);

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }

    [Fact]
    public void Get_WithEmptySummariesArray_ReturnsInternalServerError()
    {
        // Arrange
        var controller = new WeatherForecastController();
        typeof(WeatherForecastController).GetProperty("Summaries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(controller, new string[] { });

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }
}