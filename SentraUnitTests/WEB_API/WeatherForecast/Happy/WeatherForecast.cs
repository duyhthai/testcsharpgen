using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void GetTemperatureF_ReturnsCorrectValue()
        {
            // Arrange
            var forecast = new WeatherForecast
            {
                TemperatureC = 25
            };

            // Act
            var temperatureF = forecast.TemperatureF;

            // Assert
            Assert.Equal(77, temperatureF);
        }

        [Fact]
        public void SetTemperatureC_ChangesTemperatureF()
        {
            // Arrange
            var forecast = new WeatherForecast();
            forecast.TemperatureC = 25;

            // Act
            forecast.TemperatureC = 30;

            // Assert
            Assert.Equal(86, forecast.TemperatureF);
        }

        [Fact]
        public void DefaultSummary_IsNotNullOrEmpty()
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act
            var summary = forecast.Summary;

            // Assert
            Assert.NotNull(summary);
            Assert.NotEmpty(summary);
        }
    }
}