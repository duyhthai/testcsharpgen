using Microsoft.AspNetCore.Mvc;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Add_WithValidInputs_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Add_WithZeroInputs_ReturnsZero()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Add(0, 0);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_NegativeNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Add(-1, -2);

        // Assert
        Assert.Equal(-3, result);
    }
}