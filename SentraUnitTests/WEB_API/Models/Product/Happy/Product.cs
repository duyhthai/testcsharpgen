using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEB_API.Models;

namespace WEB_API.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestProductConstructor()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Sku = "SKU123",
                Content = "Sample content",
                Price = 19.99f,
                DiscountPrice = null,
                IsActive = true,
                ImageUrl = "http://example.com/image.jpg",
                ViewCount = 0,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("SKU123", product.Sku);
            Assert.AreEqual("Sample content", product.Content);
            Assert.AreEqual(19.99f, product.Price);
            Assert.IsNull(product.DiscountPrice);
            Assert.IsTrue(product.IsActive);
            Assert.AreEqual("http://example.com/image.jpg", product.ImageUrl);
            Assert.AreEqual(0, product.ViewCount);
            Assert.IsTrue(product.CreatedAt <= DateTime.Now);
        }

        [TestMethod]
        public void TestProductWithDiscount()
        {
            // Arrange
            var product = new Product
            {
                Id = 2,
                Sku = "SKU456",
                Content = "Discounted content",
                Price = 29.99f,
                DiscountPrice = 19.99f,
                IsActive = true,
                ImageUrl = "http://example.com/discount_image.jpg",
                ViewCount = 10,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(2, product.Id);
            Assert.AreEqual("SKU456", product.Sku);
            Assert.AreEqual("Discounted content", product.Content);
            Assert.AreEqual(29.99f, product.Price);
            Assert.AreEqual(19.99f, product.DiscountPrice.Value);
            Assert.IsTrue(product.IsActive);
            Assert.AreEqual("http://example.com/discount_image.jpg", product.ImageUrl);
            Assert.AreEqual(10, product.ViewCount);
            Assert.IsTrue(product.CreatedAt <= DateTime.Now);
        }

        [TestMethod]
        public void TestProductInactive()
        {
            // Arrange
            var product = new Product
            {
                Id = 3,
                Sku = "SKU789",
                Content = "Inactive content",
                Price = 14.99f,
                DiscountPrice = null,
                IsActive = false,
                ImageUrl = "http://example.com/inactive_image.jpg",
                ViewCount = 5,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(3, product.Id);
            Assert.AreEqual("SKU789", product.Sku);
            Assert.AreEqual("Inactive content", product.Content);
            Assert.AreEqual(14.99f, product.Price);
            Assert.IsNull(product.DiscountPrice);
            Assert.IsFalse(product.IsActive);
            Assert.AreEqual("http://example.com/inactive_image.jpg", product.ImageUrl);
            Assert.AreEqual(5, product.ViewCount);
            Assert.IsTrue(product.CreatedAt <= DateTime.Now);
        }
    }
}