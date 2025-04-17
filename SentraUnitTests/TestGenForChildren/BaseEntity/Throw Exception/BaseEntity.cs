using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void ThrowExceptionTest_BaseEntity_ThrowsArgumentException_WhenIdIsZero()
    {
        // Arrange
        var id = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity<int>(id));
    }

    [Fact]
    public void ThrowExceptionTest_BaseEntity_ThrowsArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var id = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity<int>(id));
    }
}