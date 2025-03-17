using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsCorrectNumberOfForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Count());
    }

    [Fact]
    public void Get_ReturnsCorrectTemperatureRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var temperatureThreshold = -20;

        // Act
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Assert
        Assert.All(forecasts, forecast => Assert.True(forecast.TemperatureC >= temperatureThreshold));
    }

    [Fact]
    public void Get_ReturnsNonEmptySummary()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Assert
        Assert.All(forecasts, forecast => Assert.NotEmpty(forecast.Summary));
    }
}