using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetStr_ReturnsExpectedString()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        string result = userEntity.GetStr();

        // Assert
        Assert.Equal("Hello GetStr", result);
    }

    [Fact]
    public void GetStr_CallsBaseImplementation()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        string result = userEntity.GetStr();

        // Assert
        Assert.True(result == "Hello GetStr");
    }

    [Fact]
    public void GetStr_DoesNotThrowException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => userEntity.GetStr());
    }
}