using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

[TestFixture]
public class ProductControllerTests
{
    private Mock<IConfiguration> _configurationMock;
    private string _connectionString;
    private ProductController _productController;

    [SetUp]
    public void SetUp()
    {
        _configurationMock = new Mock<IConfiguration>();
        _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
        _configurationMock.SetupGet(c => c.GetConnectionString("DefaultConnection")).Returns(_connectionString);
        _productController = new ProductController(_configurationMock.Object);
    }

    [Test]
    public async Task Delete_WithValidId_ShouldExecuteStoredProcedure()
    {
        // Arrange
        int validId = 1;
        var mockDbConnection = new Mock<IDbConnection>();
        var mockDapper = new Mock<IDapper>();

        _productController.DatabaseContext = mockDbConnection.Object;
        mockDbConnection.Setup(conn => conn.Open()).Verifiable();
        mockDbConnection.Setup(conn => conn.QueryFirstOrDefault(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandType>())).Returns((object)null);

        // Act
        await _productController.Delete(validId);

        // Assert
        mockDbConnection.Verify(conn => conn.Open(), Times.Once());
        mockDbConnection.Verify(conn => conn.ExecuteAsync(It.Is<string>(s => s.Contains("Delete_Product_ById")), It.IsAny<object>(), It.IsAny<SqlTransaction>(), It.IsAny<int?>()), Times.Once());
    }

    [Test]
    public async Task Delete_WithNegativeId_ShouldThrowArgumentException()
    {
        // Arrange
        int negativeId = -1;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _productController.Delete(negativeId));
    }
}