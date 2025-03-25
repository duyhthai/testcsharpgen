using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net;

public class EdgeTests
{
    [Fact]
    public async Task Should_EnableSwagger_When_EnvironmentIsDevelopment()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder(new string[] { "--environment Development" });
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

        // Assert
        var response = await app.SendAsync("GET", "/swagger/index.html");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public void Should_DisableSwagger_When_EnvironmentIsNotDevelopment()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder(new string[] { "--environment Production" });
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

        // Assert
        var response = app.SendAsync("GET", "/swagger/index.html").Result;
        Assert.Null(response);
    }
}