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
        var result = controller.Get();

        // Assert
        Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_ContainsValidTemperatureValues()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get();

        // Act
        var temperatures = result.Select(forecast => forecast.TemperatureC);

        // Assert
        foreach (var temperature in temperatures)
        {
            Assert.InRange(temperature, -20, 55);
        }
    }

    [Fact]
    public void Get_ContainsValidSummaryValues()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get();

        // Act
        var summaries = result.Select(forecast => forecast.Summary);

        // Assert
        foreach (var summary in summaries)
        {
            Assert.Contains(summary, Summaries);
        }
    }
}