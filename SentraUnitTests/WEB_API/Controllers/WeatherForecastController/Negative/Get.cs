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
    public void Get_WithNullSummary_ReturnsInternalServerError()
    {
        // Arrange
        var summaries = new string[] { null };
        var controller = new WeatherForecastController();
        controller.GetType().GetProperty("Summaries").SetValue(controller, summaries);

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }

    [Fact]
    public void Get_WithEmptySummary_ReturnsInternalServerError()
    {
        // Arrange
        var summaries = new string[] { "" };
        var controller = new WeatherForecastController();
        controller.GetType().GetProperty("Summaries").SetValue(controller, summaries);

        // Act & Assert
        var result = Assert.ThrowsAsync<InternalServerErrorObjectResult>(() => controller.Get());
    }
}