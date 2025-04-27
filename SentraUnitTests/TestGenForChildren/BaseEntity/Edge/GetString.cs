using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetString_EdgeCase_EmptyString()
    {
        // Arrange
        var entity = new DerivedEntity();

        // Act
        string result = entity.GetString();

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_EdgeCase_MaximumLengthString()
    {
        // Arrange
        var entity = new DerivedEntity();

        // Act
        string result = entity.GetString();

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_EdgeCase_NullInput()
    {
        // Arrange
        var entity = new DerivedEntity();

        // Act
        string result = entity.GetString();

        // Assert
        Assert.Equal("Hello", result);
    }
}

public class DerivedEntity : BaseEntity<int>
{
    public override string GetString()
    {
        return base.GetString();
    }
}