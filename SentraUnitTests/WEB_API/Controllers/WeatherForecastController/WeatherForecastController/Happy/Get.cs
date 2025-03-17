using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsExpectedNumberOfForecasts()
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
    public void Get_EachForecastHasValidDateAndSummary()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var forecasts = controller.Get().ToList();

        // Act & Assert
        foreach (var forecast in forecasts)
        {
            Assert.NotNull(forecast.Date);
            Assert.NotEmpty(forecast.Summary);
        }
    }

    [Fact]
    public void Get_TemperatureInRange()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var forecasts = controller.Get().ToList();

        // Act & Assert
        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}