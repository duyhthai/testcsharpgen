using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

public class WebApplicationTests
{
    [Fact]
    public void Test_WebApplication_Builder_ConfiguresServices()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var services = new ServiceCollection();
        builder.Services.AddSingleton(services);

        // Act
        builder.ConfigureServices(services => services.AddControllers());

        // Assert
        Assert.Contains(typeof(MvcServiceCollectionExtensions), builder.Services);
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithNullActionThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var app = builder.Build();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            app.MapGet("/test", null);
        });
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithEmptyPathThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        var app = builder.Build();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            app.MapGet("", () => "Hello World");
        });
    }
}