using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenNameIsNull()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = string.Empty };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenIdIsZero()
    {
        // Arrange
        var user = new UserEntity { Id = 0, Name = "John" };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.DisplayInfo());
    }
}