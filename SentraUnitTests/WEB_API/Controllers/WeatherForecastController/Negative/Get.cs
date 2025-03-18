using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsEmptyList_WhenCalled()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Get_ThrowsArgumentException_WhenInputIsInvalid()
    {
        // Arrange
        var controller = new WeatherForecastController();
        var invalidInput = "invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => controller.Get(invalidInput));
    }

    [Fact]
    public void Get_ThrowsInvalidOperationException_WhenDataAccessFails()
    {
        // Arrange
        var controller = new WeatherForecastController();
        Mock<IWeatherForecastService> mockService = new Mock<IWeatherForecastService>();
        mockService.Setup(service => service.GetData()).Throws(new InvalidOperationException());

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => controller.Get());
    }
}