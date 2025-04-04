using Microsoft.AspNetCore.Mvc;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Add_WithMinimumIntValues_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();
        int a = int.MinValue;
        int b = int.MinValue;

        // Act
        int result = controller.Add(a, b);

        // Assert
        Assert.Equal(int.MaxValue, result);
    }

    [Fact]
    public void Add_WithMaximumIntValues_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();
        int a = int.MaxValue;
        int b = int.MaxValue;

        // Act
        int result = controller.Add(a, b);

        // Assert
        Assert.Equal(-2, result);
    }

    [Fact]
    public void Add_WithLargePositiveIntegers_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();
        long a = long.MaxValue;
        long b = long.MaxValue;

        // Act
        long result = (long)controller.Add((int)a, (int)b);

        // Assert
        Assert.Equal(2 * long.MaxValue, result);
    }
}