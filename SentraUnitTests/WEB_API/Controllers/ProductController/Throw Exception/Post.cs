using NUnit.Framework;
using WEB_API.Controllers;
using WEB_API.Models;
using System.Data.SqlClient;

[TestFixture]
public class ProductControllerTests
{
    private string _connectionString = "InvalidConnectionString"; // Replace with an actual invalid connection string

    [Test]
    public void Post_WithValidProduct_ReturnsNewId()
    {
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };

        var result = controller.Post(product).Result;

        Assert.IsInstanceOf<int>(result);
        Assert.Greater(result, 0);
    }

    [Test]
    public void Post_WithNullProduct_ThrowsArgumentNullException()
    {
        var controller = new ProductController();

        Assert.Throws<ArgumentNullException>(() => controller.Post(null));
    }

    [Test]
    public void Post_WithOpenConnectionAndException_ThrowsSqlException()
    {
        var controller = new ProductController();
        var product = new Product { Sku = "SKU123", Content = "Content", Price = 100, IsActive = true, ImageUrl = "http://example.com/image.jpg" };
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@sku", product.Sku);
            parameters.Add("@content", product.Content);
            parameters.Add("@price", product.Price);
            parameters.Add("@isActive", product.IsActive);
            parameters.Add("@imageUrl", product.ImageUrl);
            parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            Assert.Throws<SqlException>(() => controller.Post(product).Wait());
        }
    }
}