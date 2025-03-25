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
        var result = product.Id == 1 && 
                     product.Sku == "SKU123" && 
                     product.Content == "Sample content" && 
                     product.Price == 19.99f && 
                     product.DiscountPrice == null && 
                     product.IsActive == true && 
                     product.ImageUrl == "http://example.com/image.jpg" && 
                     product.ViewCount == 0 && 
                     product.CreatedAt != default(DateTime);

        // Assert
        Assert.IsTrue(result);
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
            ViewCount = 0,
            CreatedAt = DateTime.Now
        };

        // Act
        var result = product.Id == 2 && 
                     product.Sku == "SKU456" && 
                     product.Content == "Discounted content" && 
                     product.Price == 29.99f && 
                     product.DiscountPrice == 19.99f && 
                     product.IsActive == true && 
                     product.ImageUrl == "http://example.com/discount-image.jpg" && 
                     product.ViewCount == 0 && 
                     product.CreatedAt != default(DateTime);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestProductConstructorWithInactiveProduct()
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
            ImageUrl = "http://example.com/inactive-image.jpg",
            ViewCount = 0,
            CreatedAt = DateTime.Now
        };

        // Act
        var result = product.Id == 3 && 
                     product.Sku == "SKU789" && 
                     product.Content == "Inactive content" && 
                     product.Price == 14.99f && 
                     product.DiscountPrice == null && 
                     product.IsActive == false && 
                     product.ImageUrl == "http://example.com/inactive-image.jpg" && 
                     product.ViewCount == 0 && 
                     product.CreatedAt != default(DateTime);

        // Assert
        Assert.IsTrue(result);
    }
}