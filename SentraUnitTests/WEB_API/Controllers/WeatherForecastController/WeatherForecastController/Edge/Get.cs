using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsCorrectNumberOfForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get().ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Get_ReturnsValidTemperatureRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get().ToList();

        // Assert
        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }

    [Fact]
    public void Get_ReturnsNonEmptySummaries()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get().ToList();

        // Assert
        foreach (var forecast in result)
        {
            Assert.NotEmpty(forecast.Summary);
        }
    }
}