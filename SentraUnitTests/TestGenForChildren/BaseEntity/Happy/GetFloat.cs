using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetFloat_ReturnsExpectedValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        int result = entity.GetFloat();

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void GetFloat_ReturnsCorrectType()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        int result = entity.GetFloat();

        // Assert
        Assert.IsType<int>(result);
    }
}

public class ConcreteEntity : BaseEntity<int>
{
    public override int GetFloat()
    {
        return base.GetFloat();
    }
}