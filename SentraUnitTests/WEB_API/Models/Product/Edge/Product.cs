using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEB_API.Models;

namespace WEB_API.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestProductConstructor_WithMinimumValues()
        {
            // Arrange
            var product = new Product
            {
                Id = 0,
                Sku = "SKU001",
                Content = "Sample content",
                Price = 0f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.MinValue
            };

            // Act
            // No action required as we're just checking the constructor doesn't throw

            // Assert
            Assert.AreEqual(0, product.Id);
            Assert.AreEqual("SKU001", product.Sku);
            Assert.AreEqual("Sample content", product.Content);
            Assert.AreEqual(0f, product.Price);
            Assert.IsNull(product.DiscountPrice);
            Assert.IsTrue(product.IsActive);
            Assert.AreEqual("http://example.com/image.jpg", product.ImageUrl);
            Assert.AreEqual(0, product.ViewCount);
            Assert.AreEqual(DateTime.MinValue, product.CreatedAt);
        }

        [TestMethod]
        public void TestProductConstructor_WithMaximumValues()
        {
            // Arrange
            var product = new Product
            {
                Id = int.MaxValue,
                Sku = new string('A', 100), // Assuming maximum length for SKU
                Content = new string('B', 1000), // Assuming maximum length for Content
                Price = float.MaxValue,
                DiscountPrice = float.MaxValue,
                IsActive = false,
                ImageUrl = new string('C', 1000), // Assuming maximum length for ImageUrl
                ViewCount = int.MaxValue,
                CreatedAt = DateTime.MaxValue
            };

            // Act
            // No action required as we're just checking the constructor doesn't throw

            // Assert
            Assert.AreEqual(int.MaxValue, product.Id);
            Assert.AreEqual(new string('A', 100), product.Sku);
            Assert.AreEqual(new string('B', 1000), product.Content);
            Assert.AreEqual(float.MaxValue, product.Price);
            Assert.AreEqual(float.MaxValue, product.DiscountPrice);
            Assert.IsFalse(product.IsActive);
            Assert.AreEqual(new string('C', 1000), product.ImageUrl);
            Assert.AreEqual(int.MaxValue, product.ViewCount);
            Assert.AreEqual(DateTime.MaxValue, product.CreatedAt);
        }
    }
}