using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void BaseEntity_WithMinValueId_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var minValue = default(int);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaseEntity<int>(minValue));
    }

    [Fact]
    public void BaseEntity_WithMaxValueId_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var maxValue = int.MaxValue;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaseEntity<int>(maxValue));
    }
}