using Xunit;
using WEB_API.Models;

namespace WEB_API.Tests
{
    public class ProductTests
    {
        [Fact]
        public void TestProductConstructor()
        {
            // Arrange
            var product = new Product();

            // Act
            var id = product.Id;
            var sku = product.Sku;
            var content = product.Content;
            var price = product.Price;
            var discountPrice = product.DiscountPrice;
            var isActive = product.IsActive;
            var imageUrl = product.ImageUrl;
            var viewCount = product.ViewCount;
            var createdAt = product.CreatedAt;

            // Assert
            Assert.NotEqual(0, id);
            Assert.NotEmpty(sku);
            Assert.NotEmpty(content);
            Assert.NotEqual(0.0f, price);
            Assert.Null(discountPrice);
            Assert.True(isActive);
            Assert.NotEmpty(imageUrl);
            Assert.Equal(0, viewCount);
            Assert.NotEqual(default(DateTime), createdAt);
        }

        [Fact]
        public void TestProductWithDiscount()
        {
            // Arrange
            var product = new Product
            {
                DiscountPrice = 50.0f
            };

            // Act
            var discountPrice = product.DiscountPrice;

            // Assert
            Assert.NotNull(discountPrice);
            Assert.Equal(50.0f, discountPrice.Value);
        }

        [Fact]
        public void TestProductDeactivation()
        {
            // Arrange
            var product = new Product
            {
                IsActive = false
            };

            // Act
            var isActive = product.IsActive;

            // Assert
            Assert.False(isActive);
        }
    }
}