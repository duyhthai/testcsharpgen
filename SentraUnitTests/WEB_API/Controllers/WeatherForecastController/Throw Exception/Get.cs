using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ThrowsException_WhenCalled()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => controller.Get());
    }
}