using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_With_Null_Id_Throws_ArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BaseEntity(default));
    }

    [Fact]
    public void Constructor_With_Empty_String_Id_Throws_ArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity(""));
    }

    [Fact]
    public void Constructor_With_Negative_Number_Id_Throws_ArgumentOutOfRangeException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaseEntity(-1));
    }
}