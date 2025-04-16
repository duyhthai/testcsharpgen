using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetStr_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.GetStr());
    }

    [Fact]
    public void GetStr_WithEmptyStringInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetStr(""));
    }

    [Fact]
    public void GetStr_WithWhitespaceInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetStr(" "));
    }
}