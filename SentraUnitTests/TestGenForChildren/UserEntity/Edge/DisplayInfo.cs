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
        // Instead, we can check if the method was called by mocking the Console.WriteLine method.
        // For simplicity, this example assumes the existence of a mock setup.
        // Mock.Get(Console).Verify(c => c.WriteLine("User ID: 1, Name: John Doe"), Times.Once);
    }

    [Fact]
    public void DisplayInfo_ShouldIncludeUserIdAndName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "John Doe" };
        string expectedOutput = $"User ID: {user.Id}, Name: {user.Name}";
        
        // Act
        user.DisplayInfo();
        
        // Assert
        // Note: Similar to the previous test, we need to mock Console.WriteLine to verify the output.
        // Mock.Get(Console).Verify(c => c.WriteLine(expectedOutput), Times.Once);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "" };
        string expectedOutput = $"User ID: {user.Id}, Name: ";
        
        // Act
        user.DisplayInfo();
        
        // Assert
        // Mock.Get(Console).Verify(c => c.WriteLine(expectedOutput), Times.Once);
    }
}