using System;
using Xunit;

namespace WEB_API.Tests
{
    public class ProductTests
    {
        [Fact]
        public void TestProductConstructorWithMinimumValues()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "SKU1",
                Content = "Content1",
                Price = 0.01f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.MinValue
            };

            // Act & Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("SKU1", product.Sku);
            Assert.Equal("Content1", product.Content);
            Assert.Equal(0.01f, product.Price);
            Assert.Null(product.DiscountPrice);
            Assert.True(product.IsActive);
            Assert.Equal("http://example.com/image.jpg", product.ImageUrl);
            Assert.Equal(0, product.ViewCount);
            Assert.Equal(DateTime.MinValue, product.CreatedAt);
        }

        [Fact]
        public void TestProductConstructorWithMaximumValues()
        {
            // Arrange
            var product = new Product
            {
                Id = int.MaxValue,
                Sku = new string('A', 100),
                Content = new string('B', 1000),
                Price = float.MaxValue,
                DiscountPrice = float.MaxValue,
                IsActive = false,
                ImageUrl = new string('C', 255),
                ViewCount = int.MaxValue,
                CreatedAt = DateTime.MaxValue
            };

            // Act & Assert
            Assert.Equal(int.MaxValue, product.Id);
            Assert.Equal(new string('A', 100), product.Sku);
            Assert.Equal(new string('B', 1000), product.Content);
            Assert.Equal(float.MaxValue, product.Price);
            Assert.Equal(float.MaxValue, product.DiscountPrice);
            Assert.False(product.IsActive);
            Assert.Equal(new string('C', 255), product.ImageUrl);
            Assert.Equal(int.MaxValue, product.ViewCount);
            Assert.Equal(DateTime.MaxValue, product.CreatedAt);
        }
    }
}