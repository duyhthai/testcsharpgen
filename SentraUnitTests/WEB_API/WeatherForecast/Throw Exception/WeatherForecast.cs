using System;
using Xunit;

namespace WEB_API.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void SetTemperatureC_WithNegativeValue_ThrowsArgumentException()
        {
            var forecast = new WeatherForecast();
            Assert.Throws<ArgumentException>(() => forecast.TemperatureC = -100);
        }

        [Fact]
        public void GetTemperatureF_WithInvalidTemperatureC_ThrowsInvalidOperationException()
        {
            var forecast = new WeatherForecast { TemperatureC = -100 };
            Assert.Throws<InvalidOperationException>(() => _ = forecast.TemperatureF);
        }

        [Fact]
        public void SetSummary_NullValue_ThrowsArgumentNullException()
        {
            var forecast = new WeatherForecast();
            Assert.Throws<ArgumentNullException>(() => forecast.Summary = null);
        }
    }
}