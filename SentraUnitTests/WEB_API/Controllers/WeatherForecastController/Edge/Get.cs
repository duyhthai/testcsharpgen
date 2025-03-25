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
    public void Get_FirstForecastDateIsTodayPlusOneDay()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var expectedDate = DateTime.Now.AddDays(1);

        // Act
        var result = controller.Get().First();

        // Assert
        Assert.Equal(expectedDate, result.Date);
    }

    [Fact]
    public void Get_TemperatureCRangeIsValid()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var minTemperature = -20;
        var maxTemperature = 55;

        // Act
        var result = controller.Get().Select(forecast => forecast.TemperatureC).ToList();

        // Assert
        foreach (var temperature in result)
        {
            Assert.InRange(temperature, minTemperature, maxTemperature);
        }
    }
}