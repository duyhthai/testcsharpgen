using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class ProductControllerTests
{
    private Mock<SqlConnection> _mockConnection;
    private Mock<IDbCommand> _mockCommand;
    private Mock<IDataReader> _mockDataReader;
    private ProductController _controller;
    private const string ConnectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";
    
    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<SqlConnection>(ConnectionString);
        _mockCommand = new Mock<IDbCommand>();
        _mockDataReader = new Mock<IDataReader>();

        _mockConnection.Setup(c => c.CreateCommand()).Returns(_mockCommand.Object);
        _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockDataReader.Object);

        _controller = new ProductController();
        _controller._connectionString = ConnectionString;
    }

    [Test]
    public async Task Get_ReturnsProduct_WithValidIdAndName()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "TestProduct" };
        var productsList = new List<Product> { product }.AsQueryable();

        _mockDataReader.Setup(dr => dr.Read()).Returns(true).Verifiable();
        _mockDataReader.SetupSequence(dr => dr["Id"]).Returns(product.Id);
        _mockDataReader.SetupSequence(dr => dr["Name"]).Returns(product.Name);

        _mockConnection.Setup(c => c.Open()).Verifiable();
        
        // Act
        var result = await _controller.Get(1, "TestProduct");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(product.Id, result.Id);
        Assert.AreEqual(product.Name, result.Name);
        _mockConnection.Verify(c => c.Open(), Times.Once());
        _mockDataReader.Verify(dr => dr.Read(), Times.Once());
        _mockDataReader.Verify(dr => dr["Id"], Times.Once());
        _mockDataReader.Verify(dr => dr["Name"], Times.Once());
    }

    [Test]
    public async Task Get_ThrowsException_WithInvalidId()
    {
        // Arrange
        var parameters = new Dictionary<string, object>
        {
            { "@id", -1 },
            { "@name", "" }
        };

        _mockCommand.Setup(cmd => cmd.Parameters).Returns(parameters.AsReadOnly());

        _mockConnection.Setup(c => c.Open()).Verifiable();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get(-1, ""));
    }

    [Test]
    public async Task Get_ThrowsException_WithEmptyName()
    {
        // Arrange
        var parameters = new Dictionary<string, object>
        {
            { "@id", 1 },
            { "@name", "" }
        };

        _mockCommand.Setup(cmd => cmd.Parameters).Returns(parameters.AsReadOnly());

        _mockConnection.Setup(c => c.Open()).Verifiable();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.Get(1, ""));
    }
}