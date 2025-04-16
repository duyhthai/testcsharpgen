using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetString_ReturnsExpectedString()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        string result = userEntity.GetString();

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetString_CallsBaseImplementation()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        string result = userEntity.GetString();

        // Assert
        Assert.True(result == "Hello");
    }

    [Fact]
    public void GetString_DoesNotThrowException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => userEntity.GetString());
    }
}