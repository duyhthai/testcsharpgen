using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsWeatherForecasts()
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
    public void Get_ThrowsException_WhenCalledWithInvalidParameter()
    {
        // Arrange
        var controller = new WeatherForecastController();
        // Note: There is no parameter in the Get method to pass a value for testing purposes.

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => controller.Get("invalid"));
    }
}