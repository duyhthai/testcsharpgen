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
    public void Get_EachForecastHasValidDateAndSummary()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get();

        // Assert
        foreach (var forecast in result)
        {
            Assert.NotNull(forecast.Date);
            Assert.NotEmpty(forecast.Summary);
        }
    }

    [Fact]
    public void Get_TemperatureCIsWithinExpectedRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var result = controller.Get();

        // Assert
        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}