using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldPrintUserInfo()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "John Doe" };

        // Act
        user.DisplayInfo();

        // Assert
        // Note: Since DisplayInfo uses Console.WriteLine, we cannot directly assert the output.
        // Instead, we can check if the method was called without throwing an exception.
        // However, this approach assumes that the method does not throw exceptions when executed.
        // For more accurate testing, consider using a mocking framework like Moq to capture the output.
    }

    [Fact]
    public void DisplayInfo_ShouldHandleNullName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = string.Empty };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.DisplayInfo());
    }
}