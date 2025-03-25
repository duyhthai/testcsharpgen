using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class WebApplicationTests
{
    [Fact]
    public void Test_WebApplication_Builder_ConfiguresServices()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var services = new ServiceCollection();
        builder.Services = services;

        // Act
        builder.ConfigureServices();

        // Assert
        Assert.Collection(services,
            service => Assert.Equal(typeof(MvcServiceCollectionExtensions), service.ImplementationType),
            service => Assert.Equal(typeof(EndpointsApiExplorerServiceCollectionExtensions), service.ImplementationType),
            service => Assert.Equal(typeof(SwaggerGenServiceCollectionExtensions), service.ImplementationType));
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithNullActionThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var app = builder.Build();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => app.MapControllers(null));
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithEmptyPathThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var app = builder.Build();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => app.MapControllers(""));
    }
}