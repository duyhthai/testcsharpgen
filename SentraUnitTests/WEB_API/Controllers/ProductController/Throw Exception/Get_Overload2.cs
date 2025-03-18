using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    [Test]
    public async Task Get_ThrowsSqlException_WhenConnectionFails()
    {
        // Arrange
        var controller = new ProductController();
        var mockConnectionString = "Invalid Connection String";
        controller._connectionString = mockConnectionString;

        // Mocking the connection to throw a SqlException
        using (var mockConn = new Mock<SqlConnection>(mockConnectionString))
        {
            mockConn.Setup(conn => conn.Open()).Throws(new SqlException());

            // Act & Assert
            await Assert.ThrowsAsync<SqlException>(() => controller.Get(1, "test"));
        }
    }

    [Test]
    public async Task Get_ThrowsInvalidOperationException_WhenNoResultsFound()
    {
        // Arrange
        var controller = new ProductController();
        var mockConnectionString = "Valid Connection String";
        controller._connectionString = mockConnectionString;

        // Mocking the connection and query to throw an InvalidOperationException
        using (var mockConn = new Mock<SqlConnection>(mockConnectionString))
        {
            mockConn.Setup(conn => conn.Open());
            mockConn.Setup(conn => conn.QueryAsync<Product>("Search_Product", It.IsAny<DynamicParameters>(), null, null, System.Data.CommandType.StoredProcedure)).ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1, "test"));
        }
    }

    [Test]
    public async Task Get_ThrowsArgumentNullException_WhenNameIsNull()
    {
        // Arrange
        var controller = new ProductController();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get(1, null));
    }
}