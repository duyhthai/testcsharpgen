using Xunit;
using System;

namespace WEB_API.Tests
{
    public class ProductTests
    {
        [Fact]
        public void ShouldThrowArgumentException_WhenSkuIsNull()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Content = "Test content",
                Price = 10.0f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "test.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.Sku = null);
        }

        [Fact]
        public void ShouldThrowArgumentOutOfRangeException_WhenPriceIsNegative()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "TESTSKU",
                Content = "Test content",
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "test.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.Price = -1.0f);
        }

        [Fact]
        public void ShouldThrowInvalidOperationException_WhenDiscountPriceIsGreaterThanPrice()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "TESTSKU",
                Content = "Test content",
                Price = 10.0f,
                IsActive = true,
                ImageUrl = "test.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => product.DiscountPrice = 11.0f);
        }
    }
}