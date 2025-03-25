using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

public class StartupTests
{
    [Fact]
    public void Should_ThrowException_When_AddingNullService()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });

        // Arrange
        Action action = () => builder.Services.AddControllers(null!);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Should_ThrowException_When_MappingNullController()
    {
        var builder = WebApplication.CreateBuilder(new string[] { });
        var app = builder.Build();

        // Arrange
        Action action = () => app.MapControllers(null!);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(action);
    }
}