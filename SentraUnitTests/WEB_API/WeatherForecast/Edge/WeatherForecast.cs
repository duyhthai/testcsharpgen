using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void GetTemperatureF_WithMinTemperatureC_ReturnsCorrectValue()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = -40 };

            // Act
            var temperatureF = forecast.TemperatureF;

            // Assert
            Assert.Equal(-40, temperatureF);
        }

        [Fact]
        public void GetTemperatureF_WithMaxTemperatureC_ReturnsCorrectValue()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 100 };

            // Act
            var temperatureF = forecast.TemperatureF;

            // Assert
            Assert.Equal(212, temperatureF);
        }

        [Fact]
        public void SetTemperatureC_WithValidRange_ChangesTemperatureF()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 0 };
            var expectedFahrenheit = 32;

            // Act
            forecast.TemperatureC = 100;
            var actualFahrenheit = forecast.TemperatureF;

            // Assert
            Assert.Equal(expectedFahrenheit, actualFahrenheit);
        }
    }
}