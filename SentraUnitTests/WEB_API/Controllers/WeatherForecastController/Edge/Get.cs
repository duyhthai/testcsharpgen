using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsFiveWeatherForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get().ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Get_ForecastDatesAreSequential()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get().ToList();

        // Act & Assert
        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].Date < result[i + 1].Date);
        }
    }

    [Fact]
    public void Get_ForecastTemperaturesAreInRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get().ToList();

        // Act & Assert
        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}