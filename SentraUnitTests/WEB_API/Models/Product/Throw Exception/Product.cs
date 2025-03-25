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
            Assert.Throws<ArgumentNullException>(() => new Product
            {
                Id = 1,
                Sku = null,
                Content = "Sample content",
                Price = 10.0f,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            });
        }

        [Test]
        public void TestProductConstructorWithEmptyContent()
        {
            Assert.Throws<ArgumentException>(() => new Product
            {
                Id = 1,
                Sku = "SKU123",
                Content = "",
                Price = 10.0f,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            });
        }

        [Test]
        public void TestProductConstructorWithNegativePrice()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product
            {
                Id = 1,
                Sku = "SKU123",
                Content = "Sample content",
                Price = -10.0f,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            });
        }
    }
}