using System;
using Xunit;

namespace WEB_API.Tests.Models
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_WithNullSku_ShouldThrowArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Product { Sku = null });
        }

        [Fact]
        public void Constructor_WithEmptySku_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { Sku = "" });
        }

        [Fact]
        public void Constructor_WithValidValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var sku = "P123";
            var content = "Sample product description.";
            var price = 19.99f;
            var discountPrice = 14.99f;
            var isActive = true;
            var imageUrl = "https://example.com/product.jpg";
            var viewCount = 5;
            var createdAt = DateTime.UtcNow;

            // Act
            var product = new Product
            {
                Id = 1,
                Sku = sku,
                Content = content,
                Price = price,
                DiscountPrice = discountPrice,
                IsActive = isActive,
                ImageUrl = imageUrl,
                ViewCount = viewCount,
                CreatedAt = createdAt
            };

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal(sku, product.Sku);
            Assert.Equal(content, product.Content);
            Assert.Equal(price, product.Price);
            Assert.Equal(discountPrice, product.DiscountPrice);
            Assert.Equal(isActive, product.IsActive);
            Assert.Equal(imageUrl, product.ImageUrl);
            Assert.Equal(viewCount, product.ViewCount);
            Assert.Equal(createdAt, product.CreatedAt);
        }
    }
}