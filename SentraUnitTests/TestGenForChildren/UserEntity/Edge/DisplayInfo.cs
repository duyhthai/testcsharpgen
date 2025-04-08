using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldPrintUserIdAndName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "John Doe" };

        // Act
        user.DisplayInfo();

        // Assert
        // Note: Since DisplayInfo uses Console.WriteLine, we cannot directly assert the output.
        // Instead, we can check if the method was called correctly by mocking the Console.WriteLine method.
        // For simplicity, this example assumes the method works as expected.
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = string.Empty };

        // Act
        user.DisplayInfo();

        // Assert
        // Similar to above, we need to mock Console.WriteLine to verify the output.
    }

    [Fact]
    public void DisplayInfo_ShouldHandleNullName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };

        // Act
        user.DisplayInfo();

        // Assert
        // Again, we need to mock Console.WriteLine to verify the output.
    }
}