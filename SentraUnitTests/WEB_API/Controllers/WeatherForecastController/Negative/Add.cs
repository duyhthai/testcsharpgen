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
        Assert.Throws<ArgumentException>(() => controller.Add("a", "b"));
    }

    [Fact]
    public void Add_WithNegativeIntegerInputs_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();
        int a = -5;
        int b = -3;

        // Act
        int result = controller.Add(a, b);

        // Assert
        Assert.Equal(-8, result);
    }

    [Fact]
    public void Add_WithLargeIntegerInputs_ThrowsOverflowException()
    {
        // Arrange
        var controller = new WeatherForecastController();
        long a = long.MaxValue;
        long b = 1;

        // Act & Assert
        Assert.Throws<OverflowException>(() => controller.Add((int)a, (int)b));
    }
}