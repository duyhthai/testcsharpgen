using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsEmptyArray_WhenInputIsInvalid()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Get_ThrowsArgumentException_WhenInputIsNull()
    {
        // Arrange
        var controller = new WeatherForecastController();
        controller.Request.HttpContext = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => controller.Get());
    }

    [Fact]
    public void Get_ThrowsInvalidOperationException_WhenSummariesListIsEmpty()
    {
        // Arrange
        var controller = new WeatherForecastController();
        typeof(WeatherForecastController).GetField("Summaries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                        .SetValue(controller, new string[] { });

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => controller.Get());
    }
}