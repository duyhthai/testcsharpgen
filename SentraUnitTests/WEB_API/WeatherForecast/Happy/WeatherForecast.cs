using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void GetTemperatureF_CelsiusToFarhenheitConversion()
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
        public void SetDate_UpdatesDateProperty()
        {
            // Arrange
            var forecast = new WeatherForecast();
            var newDate = DateTime.Now.AddDays(1);

            // Act
            forecast.Date = newDate;

            // Assert
            Assert.Equal(newDate, forecast.Date);
        }

        [Fact]
        public void SetSummary_UpdatesSummaryProperty()
        {
            // Arrange
            var forecast = new WeatherForecast();
            var newSummary = "Sunny";

            // Act
            forecast.Summary = newSummary;

            // Assert
            Assert.Equal(newSummary, forecast.Summary);
        }
    }
}