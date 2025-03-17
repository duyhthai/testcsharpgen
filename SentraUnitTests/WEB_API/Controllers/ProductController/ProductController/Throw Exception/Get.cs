using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Get_ThrowsSqlException_WhenDatabaseConnectionFails()
    {
        // Arrange
        var controller = CreateProductController();
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Throws(new SqlException());
        controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);

        // Act & Assert
        await Assert.ThrowsAsync<SqlException>(() => controller.Get(1));
    }

    [Fact]
    public async Task Get_ThrowsInvalidOperationException_WhenNoRecordFound()
    {
        // Arrange
        var controller = CreateProductController();
        var mockConnection = new Mock<SqlConnection>();
        mockConnection.Setup(conn => conn.Open()).Verifiable();
        controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(SqlConnection)).Returns(mockConnection.Object);
        
        var mockDapper = new Mock<IDapper>();
        mockDapper.Setup(dapper => dapper.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CommandType?>()))
                   .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));
        controller.ControllerContext.HttpContext.RequestServices.GetService(typeof(IDapper)).Returns(mockDapper.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Get(1));
    }

    private ProductController CreateProductController()
    {
        var mockHttpContext = new Mock<IHttpContextAccessor>();
        mockHttpContext.Setup(ctx => ctx.HttpContext.RequestServices.GetService(typeof(ProductController))).Returns(new ProductController());
        return new ProductController { ControllerContext = new ControllerContext { HttpContext = mockHttpContext.Object } };
    }
}