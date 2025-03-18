using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsFiveWeatherForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as IActionResult;
        var weatherForecasts = result.Value as WeatherForecast[];

        // Assert
        Assert.NotNull(weatherForecasts);
        Assert.Equal(5, weatherForecasts.Length);
    }

    [Fact]
    public void Get_EachForecastHasValidDateAndSummary()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get() as IActionResult;
        var weatherForecasts = result.Value as WeatherForecast[];

        // Assert
        foreach (var forecast in weatherForecasts)
        {
            Assert.True(forecast.Date >= DateTime.Now && forecast.Date <= DateTime.Now.AddDays(5));
            Assert.NotEmpty(forecast.Summary);
        }
    }

    [Fact]
    public void Get_TemperatureCInRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get() as IActionResult;
        var weatherForecasts = result.Value as WeatherForecast[];

        // Assert
        foreach (var forecast in weatherForecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}