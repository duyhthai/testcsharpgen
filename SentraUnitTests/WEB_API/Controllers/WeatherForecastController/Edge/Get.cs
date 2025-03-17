using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WeatherForecastControllerTests
{
    [Trait("Category", "Edge")]
    public class WeatherForecastControllerEdgeTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerEdgeTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _controller = new WeatherForecastController(_httpContextAccessorMock.Object);
        }

        [Fact]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Arrange
            var expectedCount = 5;

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<IEnumerable<WeatherForecast>>(result);
            Assert.Equal(expectedCount, result.Count());
        }

        [Fact]
        public void Get_FirstForecastDateIsTodayPlusOneDay()
        {
            // Arrange
            var today = DateTime.Today;

            // Act
            var result = _controller.Get().First();

            // Assert
            Assert.Equal(today.AddDays(1), result.Date);
        }

        [Fact]
        public void Get_LastForecastDateIsTodayPlusFiveDays()
        {
            // Arrange
            var today = DateTime.Today;

            // Act
            var result = _controller.Get().Last();

            // Assert
            Assert.Equal(today.AddDays(5), result.Date);
        }
    }
}