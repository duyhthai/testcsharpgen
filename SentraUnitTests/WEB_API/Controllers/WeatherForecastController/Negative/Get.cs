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
    public void Get_WithEmptySummariesArray_ReturnsInternalServerError()
    {
        // Arrange
        var summaries = new string[] { };
        typeof(WeatherForecastController).GetProperty("Summaries").SetValue(null, summaries);

        var controller = new WeatherForecastController();

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }

    [Fact]
    public void Get_WithNullSummariesProperty_ReturnsInternalServerError()
    {
        // Arrange
        typeof(WeatherForecastController).GetProperty("Summaries").SetValue(null, null);

        var controller = new WeatherForecastController();

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }
}