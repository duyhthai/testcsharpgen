using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsExpectedNumberOfForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get().ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Get_ReturnsCorrectTemperatureRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var forecasts = controller.Get().ToList();

        // Assert
        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }

    [Fact]
    public void Get_ReturnsNonEmptySummaries()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var forecasts = controller.Get().ToList();

        // Assert
        foreach (var forecast in forecasts)
        {
            Assert.NotEmpty(forecast.Summary);
        }
    }
}