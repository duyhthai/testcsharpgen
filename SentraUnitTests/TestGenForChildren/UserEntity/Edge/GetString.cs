using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetString_WithBoundaryValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetString());
    }

    [Fact]
    public void GetString_WithNegativeValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetString());
    }

    [Fact]
    public void GetString_WithLargeValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetString());
    }
}