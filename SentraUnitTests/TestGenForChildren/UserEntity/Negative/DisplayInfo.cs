using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenIdIsNegative()
    {
        // Arrange
        var user = new UserEntity { Id = -1, Name = "John" };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => user.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenNameIsNull()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "" };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.DisplayInfo());
    }
}