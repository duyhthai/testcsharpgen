using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void SetTemperatureC_WithNegativeValue_ThrowsArgumentException()
        {
            // Arrange
            var forecast = new WeatherForecast();
            int negativeTemperature = -100;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => forecast.TemperatureC = negativeTemperature);
        }

        [Fact]
        public void GetTemperatureF_WithInvalidTemperatureC_ThrowsInvalidOperationException()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = int.MinValue };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _ = forecast.TemperatureF);
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