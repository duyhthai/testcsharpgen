using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsExpectedNumberOfWeatherForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get().ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Get_ThrowsException_WhenInputIsInvalid()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var mockRequest = new HttpRequestMessage(HttpMethod.Get, "api/WeatherForecast?invalidParam=value");

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get());
        Assert.Contains("Invalid input parameter", ex.Result.Message);
    }

    [Fact]
    public void Get_ReturnsCorrectTemperatureRange()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get().ToList();

        // Assert
        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }
}