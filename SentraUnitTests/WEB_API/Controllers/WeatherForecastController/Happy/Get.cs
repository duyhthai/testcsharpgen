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
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Count());
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
    public void Get_ForecastSummariesAreNotEmpty()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get() as IActionResult;
        var forecasts = result.Value as IEnumerable<WeatherForecast>;

        // Act & Assert
        foreach (var forecast in forecasts)
        {
            Assert.NotEmpty(forecast.Summary);
        }
    }
}