using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void SetTemperatureC_NegativeValue_ThrowsArgumentException()
        {
            // Arrange
            var forecast = new WeatherForecast();
            int negativeTemperature = -100;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => forecast.TemperatureC = negativeTemperature);
        }

        [Fact]
        public void SetSummary_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => forecast.Summary = null);
        }
    }
}