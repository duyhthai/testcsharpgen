using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetString_ReturnsExpectedString()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        string result = entity.GetString();

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_CallsBaseImplementation()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        string result = entity.GetString();

        // Assert
        Assert.True(result == "Hello");
    }
}

public class ConcreteEntity : BaseEntity<int>
{
    public override string GetString()
    {
        return base.GetString();
    }
}