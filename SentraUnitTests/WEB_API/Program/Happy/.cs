using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

public class WebApplicationTests
{
    [Fact]
    public void ConfigureServices_AddsControllers()
    {
        // Arrange
        var builder = new WebApplicationBuilder(new string[] { });

        // Act
        builder.Services.AddControllers();

        // Assert
        var serviceDescriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(Microsoft.AspNetCore.Mvc.Controllers.ControllerActionInvokerProvider));
        Assert.NotNull(serviceDescriptor);
    }

    [Fact]
    public void ConfigureServices_AddsEndpointsApiExplorer()
    {
        // Arrange
        var builder = new WebApplicationBuilder(new string[] { });

        // Act
        builder.Services.AddEndpointsApiExplorer();

        // Assert
        var serviceDescriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(Microsoft.OpenApi.Models.OpenApiDocument));
        Assert.NotNull(serviceDescriptor);
    }

    [Fact]
    public void ConfigureServices_AddsSwaggerGen()
    {
        // Arrange
        var builder = new WebApplicationBuilder(new string[] { });

        // Act
        builder.Services.AddSwaggerGen();

        // Assert
        var serviceDescriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(Microsoft.OpenApi.Models.OpenApiDocument));
        Assert.NotNull(serviceDescriptor);
    }
}