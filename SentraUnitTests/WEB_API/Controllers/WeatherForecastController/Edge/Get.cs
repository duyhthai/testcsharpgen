using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsTenWeatherForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(10, forecasts.Count());
    }

    [Fact]
    public void Get_ForecastDatesAreSequential()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Act & Assert
        for (int i = 0; i < forecasts.Count(); i++)
        {
            Assert.Equal(DateTime.Now.AddDays(i + 1), forecasts.ElementAt(i).Date);
        }
    }

    [Fact]
    public void Get_ForecastTemperaturesAreInRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Act & Assert
        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -30, 65);
        }
    }
}