using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Test_BaseEntity_Constructor_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        var invalidId = default(int);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity<int>(invalidId));
    }

    [Fact]
    public void Test_BaseEntity_GetInt_WithNegativeValue_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var negativeId = -1;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaseEntity<int>(negativeId).GetInt());
    }
}