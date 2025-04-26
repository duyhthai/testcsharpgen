using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_With_Maximum_Value_Id_Does_Not_Throw()
    {
        // Arrange
        var maxIntValue = int.MaxValue;

        // Act & Assert
        Assert.DoesNotThrow(() => new BaseEntity(maxIntValue));
    }

    [Fact]
    public void Constructor_With_Minimum_Value_Id_Does_Not_Throw()
    {
        // Arrange
        var minIntValue = int.MinValue;

        // Act & Assert
        Assert.DoesNotThrow(() => new BaseEntity(minIntValue));
    }

    [Fact]
    public void Constructor_With_Zero_Id_Does_Not_Throw()
    {
        // Arrange
        var zeroValue = 0;

        // Act & Assert
        Assert.DoesNotThrow(() => new BaseEntity(zeroValue));
    }
}