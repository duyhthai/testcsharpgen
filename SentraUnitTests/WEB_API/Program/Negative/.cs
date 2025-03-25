using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

public class WebApplicationBuilderTests
{
    [Fact]
    public void Should_ThrowException_When_AddingNullService()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        Action action = () => builder.Services.Add(null!);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Should_ThrowException_When_MappingNullController()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });
        var app = builder.Build();

        // Arrange
        Action action = () => app.MapControllerRoute(name: null!, pattern: "{controller=Home}/{action=Index}");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }
}