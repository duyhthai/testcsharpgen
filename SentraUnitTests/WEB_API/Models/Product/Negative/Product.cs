using NUnit.Framework;
using System;

namespace WEB_API.Tests.Models
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void TestProductConstructor_WithNullSku_ShouldThrowArgumentNullException()
        {
            // Arrange
            var productId = 1;
            var content = "Sample content";
            var price = 19.99f;
            var isActive = true;
            var imageUrl = "http://example.com/image.jpg";
            var viewCount = 10;
            var createdAt = DateTime.Now;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Product
            {
                Id = productId,
                Sku = null,
                Content = content,
                Price = price,
                IsActive = isActive,
                ImageUrl = imageUrl,
                ViewCount = viewCount,
                CreatedAt = createdAt
            });
        }

        [Test]
        public void TestProductConstructor_WithNegativePrice_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var productId = 1;
            var sku = "ABC123";
            var content = "Sample content";
            var price = -19.99f;
            var isActive = true;
            var imageUrl = "http://example.com/image.jpg";
            var viewCount = 10;
            var createdAt = DateTime.Now;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product
            {
                Id = productId,
                Sku = sku,
                Content = content,
                Price = price,
                IsActive = isActive,
                ImageUrl = imageUrl,
                ViewCount = viewCount,
                CreatedAt = createdAt
            });
        }

        [Test]
        public void TestProductConstructor_WithInvalidImageUrl_ShouldNotThrowException()
        {
            // Arrange
            var productId = 1;
            var sku = "ABC123";
            var content = "Sample content";
            var price = 19.99f;
            var isActive = true;
            var imageUrl = "invalid-url"; // Invalid URL
            var viewCount = 10;
            var createdAt = DateTime.Now;

            // Act & Assert
            Assert.DoesNotThrow(() => new Product
            {
                Id = productId,
                Sku = sku,
                Content = content,
                Price = price,
                IsActive = isActive,
                ImageUrl = imageUrl,
                ViewCount = viewCount,
                CreatedAt = createdAt
            });
        }
    }
}