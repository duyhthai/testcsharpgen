using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ThrowsException()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act & Assert
        var ex = Assert.Throws<NotImplementedException>(() => controller.Get());
        Assert.Equal("Get method is not implemented.", ex.Message);
    }

    [Fact]
    public void Get_ReturnsCorrectNumberOfForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_ReturnsValidForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.NotNull(forecast.Date);
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.NotEmpty(forecast.Summary);
        });
    }
}