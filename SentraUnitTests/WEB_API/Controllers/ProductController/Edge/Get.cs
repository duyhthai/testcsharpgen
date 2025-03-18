using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

[Collection("ProductControllerTests")]
public class ProductControllerTests
{
    private readonly string _connectionString = "Server=.;Database=TestDB;Trusted_Connection=True;";

    [Fact]
    public async Task Get_ReturnsEmptyList_WhenNoProductsExist()
    {
        // Arrange
        using (var context = new Mock<DbContext>(_connectionString))
        {
            var controller = new ProductController(context.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Empty(result);
        }
    }

    [Fact]
    public async Task Get_ReturnsListOfProducts_WhenProductsExist()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1" },
            new Product { Id = 2, Name = "Product2" }
        };

        using (var context = new Mock<DbContext>(_connectionString))
        {
            context.Setup(c => c.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                   .Returns(Task.FromResult<IEnumerable<Product>>(products));

            var controller = new ProductController(context.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }

    [Fact]
    public async Task Get_CallsOpenConnection_WhenConnectionIsClosed()
    {
        // Arrange
        using (var mockConn = new Mock<SqlConnection>(_connectionString))
        {
            mockConn.Setup(m => m.State).Returns(System.Data.ConnectionState.Closed);
            mockConn.Setup(m => m.Open()).Verifiable();
            using (var context = new Mock<DbContext>(_connectionString))
            {
                context.Setup(c => c.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<System.Data.CommandType>()))
                       .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

                var controller = new ProductController(context.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                };
                controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Should().Be(mockConn.Object);

                // Act
                await controller.Get();

                // Assert
                mockConn.Verify(m => m.Open(), Times.Once);
            }
        }
    }
}

public class DbContext : IDbConnection
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Close() { }

    public int Execute(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Query<T>(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public T ExecuteScalar<T>(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public Task<T> ExecuteScalarAsync<T>(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public void Open() { }

    public string ConnectionString => _connectionString;

    public int State => 0;

    public DbProviderFactory Provider => null;

    public IDbCommand CreateCommand()
    {
        throw new NotImplementedException();
    }

    public Task<int> ExecuteAsync(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public Task<int> ExecuteScalarAsync(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        throw new NotImplementedException();
    }

    public void Dispose() { }
}