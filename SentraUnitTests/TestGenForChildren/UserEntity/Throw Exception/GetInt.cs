using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void GetInt_ThrowsNotSupportedException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => userEntity.GetInt());
    }
}