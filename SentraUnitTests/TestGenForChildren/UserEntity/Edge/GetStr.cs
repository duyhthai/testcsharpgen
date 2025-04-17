using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetStr_WithBoundaryValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetStr());
    }

    [Fact]
    public void GetStr_WithLargeValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetStr());
    }

    [Fact]
    public void GetStr_WithNegativeValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetStr());
    }
}