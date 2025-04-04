using Microsoft.AspNetCore.Mvc;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Add_WithNonIntegerInputs_ThrowsArgumentException()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => controller.Add(1, "a"));
    }

    [Fact]
    public void Add_WithLargeIntegerInputs_ThrowsOverflowException()
    {
        // Arrange
        var controller = new WeatherForecastController();
        int maxInt = int.MaxValue;
        int largeValue = maxInt + 1;

        // Act & Assert
        Assert.Throws<OverflowException>(() => controller.Add(maxInt, largeValue));
    }
}