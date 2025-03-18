using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbConnection> _mockDbConnection;
    private string _connectionString = "your_connection_string_here";

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>(_connectionString);
        _mockDbConnection = new Mock<IDbConnection>();
    }

    [Test]
    public async Task Get_ThrowsException_WhenSqlConnectionFailsToOpen()
    {
        // Arrange
        var productController = new ProductController();
        _mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        productController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        Assert.ThrowsAsync<SqlException>(() => productController.Get(1));
    }

    [Test]
    public async Task Get_ThrowsException_WhenNoProductFound()
    {
        // Arrange
        var productController = new ProductController();
        var parameters = new DynamicParameters();
        parameters.Add("@id", 1);
        _mockDbConnection.Setup(conn => conn.QueryAsync<Product>("Get_Product_ById", parameters, null, null, System.Data.CommandType.StoredProcedure))
                         .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));
        productController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => productController.Get(1));
    }
}