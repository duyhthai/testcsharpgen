using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Xunit;

public class WebApplicationBuilderTests
{
    [Fact]
    public async Task Should_AddControllers_ToServices()
    {
        // Arrange
        var args = new string[] { };
        var builder = WebApplication.CreateBuilder(args);
        
        // Act
        builder.Services.AddControllers();
        
        // Assert
        var serviceDescriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(Microsoft.AspNetCore.Mvc.Controllers.ControllerActionInvokerProvider));
        Assert.NotNull(serviceDescriptor);
    }

    [Fact]
    public async Task Should_EnableSwagger_When_EnvironmentIsDevelopment()
    {
        // Arrange
        var args = new string[] { };
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:5000");
        
        // Act
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        // Assert
        var middleware = app.RequestPipeline.Middlewares.FirstOrDefault(m => m.GetType().Name.Contains("UseSwagger"));
        Assert.NotNull(middleware);
    }

    [Fact]
    public async Task Should_MapControllers()
    {
        // Arrange
        var args = new string[] { };
        var builder = WebApplication.CreateBuilder(args);
        
        // Act
        var app = builder.Build();
        app.MapControllers();
        
        // Assert
        var endpoint = app.EndpointRouting.RouteTable.Endpoints.FirstOrDefault(e => e.DisplayName.Contains("MapControllers"));
        Assert.NotNull(endpoint);
    }
}