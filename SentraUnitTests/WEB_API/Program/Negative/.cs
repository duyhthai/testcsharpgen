using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class WebApplicationBuilderTests
{
    [Fact]
    public void Test_WebApplication_Builder_AddsInvalidServiceThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        Action action = () => builder.Services.AddSingleton(null);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithNullActionThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });
        var app = builder.Build();

        // Arrange
        Action action = () => app.MapControllers(null);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Test_WebApplication_Builder_ConfiguresEndpointsWithEmptyPathThrowsException()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });
        var app = builder.Build();

        // Arrange
        Action action = () => app.MapGet("", () => "Hello World");

        // Act & Assert
        Assert.Throws<ArgumentException>(action);
    }
}