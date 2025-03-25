using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Xunit;

public class StartupTests
{
    [Fact]
    public void ConfigureServices_ThrowsException_WhenNullArgumentProvided()
    {
        // Arrange
        var builder = new WebApplicationBuilder(new string[] { });

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder.Services.AddControllers(null));
        Assert.Throws<ArgumentNullException>(() => builder.Services.AddEndpointsApiExplorer(null));
        Assert.Throws<ArgumentNullException>(() => builder.Services.AddSwaggerGen(null));
    }

    [Fact]
    public void MapControllers_ThrowsException_WhenNullArgumentProvided()
    {
        // Arrange
        var app = new WebApplication(new WebApplicationOptions { EnvironmentName = "Development" });

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => app.MapControllers(null));
    }
}