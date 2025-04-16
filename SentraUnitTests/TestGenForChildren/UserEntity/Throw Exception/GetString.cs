using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetString_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.GetString());
    }

    [Fact]
    public void GetString_WithEmptyStringInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetString(""));
    }

    [Fact]
    public void GetString_WithWhitespaceInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetString(" "));
    }
}