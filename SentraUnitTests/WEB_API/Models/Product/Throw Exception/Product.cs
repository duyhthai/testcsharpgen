using System;
using Xunit;

namespace WEB_API.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentException_WhenSkuIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { Sku = null });
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenContentIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { Content = null });
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenImageUrlIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { ImageUrl = null });
        }
    }
}