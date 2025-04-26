using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_With_Maximum_Value_Id()
    {
        // Arrange
        var maxIntValue = int.MaxValue;
        
        // Act
        var entity = new BaseEntity(maxIntValue);
        
        // Assert
        Assert.Equal(maxIntValue, entity.Id);
    }

    [Fact]
    public void Constructor_With_Minimum_Value_Id()
    {
        // Arrange
        var minIntValue = int.MinValue;
        
        // Act
        var entity = new BaseEntity(minIntValue);
        
        // Assert
        Assert.Equal(minIntValue, entity.Id);
    }

    [Fact]
    public void Constructor_With_Zero_Id()
    {
        // Arrange
        var zeroValue = 0;
        
        // Act
        var entity = new BaseEntity(zeroValue);
        
        // Assert
        Assert.Equal(zeroValue, entity.Id);
    }
}