using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Xunit;

public class WebApplicationTests
{
    [Fact]
    public async Task Test_WebApplication_Builder_ConfiguresServices()
    {
        // Arrange
        var args = new string[] { };
        var builder = WebApplication.CreateBuilder(args);

        // Act
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Assert
        Assert.NotNull(builder.Services);
        Assert.Contains(typeof(MvcServiceCollectionExtensions), builder.Services.GetRequiredService<IServiceCollection>().Where(s => s.ServiceType == typeof(IMvcCoreBuilder)));
        Assert.Contains(typeof(EndpointsApiExplorerServiceCollectionExtensions), builder.Services.GetRequiredService<IServiceCollection>().Where(s => s.ServiceType == typeof(IEndpointsApiExplorerBuilder)));
        Assert.Contains(typeof(SwaggerGenServiceCollectionExtensions), builder.Services.GetRequiredService<IServiceCollection>().Where(s => s.ServiceType == typeof(ISwaggerGenOptions));
    }

    [Fact]
    public async Task Test_WebApplication_BuildsAndRuns()
    {
        // Arrange
        var args = new string[] { };
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Act
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Assert
        Assert.NotNull(app);
        Assert.True(app.Environment.IsDevelopment());
    }

    [Fact]
    public async Task Test_WebApplication_RunsInProductionMode()
    {
        // Arrange
        var args = new string[] { "--environment", "Production" };
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Act
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        // Assert
        Assert.NotNull(app);
        Assert.False(app.Environment.IsDevelopment());
    }
}