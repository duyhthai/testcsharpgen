using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsEmptyArray_WhenNoData()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Get_ThrowsArgumentNullException_WhenNullSummaries()
    {
        // Arrange
        var controller = new WeatherForecastController();
        typeof(WeatherForecastController).GetProperty("Summaries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(controller, null);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => controller.Get());
    }

    [Fact]
    public void Get_ThrowsArgumentOutOfRangeException_WhenNegativeTemperature()
    {
        // Arrange
        var controller = new WeatherForecastController();
        typeof(WeatherForecastController).GetProperty("Summaries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(controller, Summaries);
        typeof(WeatherForecastController).GetMethod("Get").Invoke(controller, null);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            foreach (var forecast in controller.Get())
            {
                if (forecast.TemperatureC < -20)
                    throw new ArgumentOutOfRangeException(nameof(forecast.TemperatureC));
            }
        });
    }
}