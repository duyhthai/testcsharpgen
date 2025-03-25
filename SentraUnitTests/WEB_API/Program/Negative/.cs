using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class NegativeTests
{
    [Fact]
    public void ConfigureServices_ThrowsException_WhenNullArgumentProvided()
    {
        // Arrange
        var builder = null as WebApplicationBuilder;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder?.Services.AddControllers());
    }

    [Fact]
    public void MapControllers_ThrowsException_WhenNullArgumentProvided()
    {
        // Arrange
        var app = null as IApplicationBuilder;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => app?.MapControllers());
    }
}