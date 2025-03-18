using NUnit.Framework;
using WEB_API.Models;

namespace WEB_API.Tests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Constructor_WithNullSku_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { Sku = null });
        }

        [Test]
        public void Constructor_WithEmptySku_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Product { Sku = "" });
        }

        [Test]
        public void Constructor_WithNegativePrice_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product { Price = -1 });
        }
    }
}