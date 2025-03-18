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
        var result = controller.Get() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<WeatherForecast>>(result.Value);
        Assert.Equal(5, ((IEnumerable<WeatherForecast>)result.Value).Count());
    }

    [Fact]
    public void Get_ReturnsForecastsWithValidDates()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var now = DateTime.UtcNow;

        // Act
        var result = controller.Get() as OkObjectResult;
        var forecasts = (IEnumerable<WeatherForecast>)result.Value;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<WeatherForecast>>(result.Value);
        foreach (var forecast in forecasts)
        {
            Assert.True(forecast.Date >= now && forecast.Date <= now.AddHours(5));
        }
    }

    [Fact]
    public void Get_ReturnsForecastsWithValidTemperatures()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get() as OkObjectResult;
        var forecasts = (IEnumerable<WeatherForecast>)result.Value;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<WeatherForecast>>(result.Value);
        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}