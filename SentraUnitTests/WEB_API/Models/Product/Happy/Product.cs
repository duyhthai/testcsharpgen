using NUnit.Framework;
using WEB_API.Models;

[TestFixture]
public class ProductTests
{
    [Test]
    public void TestProductConstructorWithValidInputs()
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

        // Act
        var result = product;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("SKU123", result.Sku);
        Assert.AreEqual("Sample content", result.Content);
        Assert.AreEqual(19.99f, result.Price);
        Assert.IsNull(result.DiscountPrice);
        Assert.IsTrue(result.IsActive);
        Assert.AreEqual("http://example.com/image.jpg", result.ImageUrl);
        Assert.AreEqual(0, result.ViewCount);
        Assert.IsNotEmpty(result.CreatedAt);
    }

    [Test]
    public void TestProductConstructorWithDiscountPrice()
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
            ImageUrl = "http://example.com/discount-image.jpg",
            ViewCount = 5,
            CreatedAt = DateTime.Now
        };

        // Act
        var result = product;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Id);
        Assert.AreEqual("SKU456", result.Sku);
        Assert.AreEqual("Discounted content", result.Content);
        Assert.AreEqual(29.99f, result.Price);
        Assert.AreEqual(19.99f, result.DiscountPrice.Value);
        Assert.IsTrue(result.IsActive);
        Assert.AreEqual("http://example.com/discount-image.jpg", result.ImageUrl);
        Assert.AreEqual(5, result.ViewCount);
        Assert.IsNotEmpty(result.CreatedAt);
    }
}