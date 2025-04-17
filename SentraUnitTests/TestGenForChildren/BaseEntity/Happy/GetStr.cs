using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetStr_ReturnsExpectedString()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        string result = entity.GetStr();

        // Assert
        Assert.Equal("Hello GetStr", result);
    }

    [Fact]
    public void GetStr_CallsBaseImplementation()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        string result = entity.GetStr();

        // Assert
        Assert.True(result == "Hello GetStr");
    }
}

public class ConcreteEntity : BaseEntity<int>
{
    public override string GetStr()
    {
        return base.GetStr();
    }
}