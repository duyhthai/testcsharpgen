using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithNullId_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullId = default(int?);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BaseEntity<int>(nullId).DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidId = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity<int>(invalidId).DisplayInfo());
    }
}