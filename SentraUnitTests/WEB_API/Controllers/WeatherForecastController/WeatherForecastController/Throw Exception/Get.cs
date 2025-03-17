using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WeatherForecast.Tests.Controllers
{
    [Collection("Sequential")]
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _controller = new WeatherForecastController(_httpContextAccessorMock.Object);
        }

        [Fact]
        public void Get_ReturnsIEnumerableOfWeatherForecast()
        {
            // Arrange
            var expectedCount = 5;

            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
            Assert.Equal(expectedCount, result.Count());
        }

        [Fact]
        public void Get_ThrowsInvalidOperationException_WhenHttpContextIsNull()
        {
            // Arrange
            _httpContextAccessorMock.Setup(m => m.HttpContext).Returns((HttpContext)null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _controller.Get());
        }

        [Fact]
        public void Get_ThrowsArgumentNullException_WhenSummariesArrayIsNull()
        {
            // Arrange
            var controller = new WeatherForecastController(null);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => controller.Get());
        }
    }
}