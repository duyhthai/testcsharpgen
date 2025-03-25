using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void SetDate_WithMinDateTime_UpdatesDateProperty()
        {
            // Arrange
            var forecast = new WeatherForecast();
            var minDateTime = DateTime.MinValue;

            // Act
            forecast.Date = minDateTime;

            // Assert
            Assert.Equal(minDateTime, forecast.Date);
        }

        [Fact]
        public void SetDate_WithMaxDateTime_UpdatesDateProperty()
        {
            // Arrange
            var forecast = new WeatherForecast();
            var maxDateTime = DateTime.MaxValue;

            // Act
            forecast.Date = maxDateTime;

            // Assert
            Assert.Equal(maxDateTime, forecast.Date);
        }

        [Fact]
        public void GetTemperatureF_WithMinTemperatureC_ReturnsCorrectTemperatureF()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = int.MinValue };

            // Act
            var temperatureF = forecast.TemperatureF;

            // Assert
            Assert.Equal(32, temperatureF);
        }
    }
}