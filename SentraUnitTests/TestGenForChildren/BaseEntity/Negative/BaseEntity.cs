using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void NegativeTest_BaseEntity_ThrowsArgumentException_WhenIdIsNegative()
    {
        // Arrange
        var negativeId = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity<int>(negativeId));
    }

    [Fact]
    public void NegativeTest_BaseEntity_ThrowsArgumentNullException_WhenIdIsNull()
    {
        // Arrange
        object nullId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BaseEntity<object>(nullId));
    }
}