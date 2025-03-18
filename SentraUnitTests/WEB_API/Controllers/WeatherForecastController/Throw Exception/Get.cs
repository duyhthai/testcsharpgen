using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_Throws_NotImplementedException()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => controller.Get());
    }
}