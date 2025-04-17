using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Test_BaseEntity_Constructor_WithInvalidId()
    {
        // Arrange
        var invalidId = (int?)null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BaseEntity<int>(invalidId));
    }

    [Fact]
    public void Test_BaseEntity_GetInt_WithNegativeValue()
    {
        // Arrange
        var baseEntity = new BaseEntity<int>(0);

        // Act
        var result = baseEntity.GetInt();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_BaseEntity_DisplayInfo_ThrowsException()
    {
        // Arrange
        var baseEntity = new BaseEntity<int>(0);

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => baseEntity.DisplayInfo());
    }
}