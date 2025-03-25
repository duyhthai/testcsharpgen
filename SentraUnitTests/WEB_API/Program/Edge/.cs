using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class EdgeTests
{
    [Fact]
    public void MapControllers_WithEmptyServiceCollection_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection();
        var provider = services.BuildServiceProvider();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => provider.GetRequiredService<IEndpointRouteBuilder>().MapControllers());
    }

    [Fact]
    public void MapControllers_WithNullArgument_ThrowsArgumentNullException()
    {
        // Arrange
        IEndpointRouteBuilder endpointRouteBuilder = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => endpointRouteBuilder.MapControllers());
    }
}