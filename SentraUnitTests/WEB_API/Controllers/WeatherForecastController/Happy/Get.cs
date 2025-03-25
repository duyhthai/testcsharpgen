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
        Assert.IsType<IEnumerable<WeatherForecast>>(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_FirstForecastDateIsTodayPlusOneDay()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var today = DateTime.Today;

        // Act
        var result = controller.Get();
        var firstForecast = result.First();

        // Assert
        Assert.Equal(today.AddDays(1), firstForecast.Date);
    }

    [Fact]
    public void Get_TemperatureCRangeIsValid()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}