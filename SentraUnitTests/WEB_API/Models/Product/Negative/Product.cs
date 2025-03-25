using NUnit.Framework;
using System;

namespace WEB_API.Tests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void TestProductConstructorWithNullSku()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = null,
                Content = "Sample content",
                Price = 10.0f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => product.Sku);
        }

        [Test]
        public void TestProductConstructorWithEmptyContent()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "SKU123",
                Content = "",
                Price = 10.0f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.Content);
        }

        [Test]
        public void TestProductConstructorWithNegativePrice()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "SKU123",
                Content = "Sample content",
                Price = -10.0f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.Price);
        }
    }
}