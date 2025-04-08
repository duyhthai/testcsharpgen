using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldPrintUserInfo()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "John Doe" };
        var expectedOutput = $"User ID: {user.Id}, Name: {user.Name}";

        // Act
        using (var output = new System.IO.StringWriter())
        {
            Console.SetOut(output);
            user.DisplayInfo();
            var actualOutput = output.ToString().TrimEnd();
        }

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleNullName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };
        var expectedOutput = $"User ID: {user.Id}, Name: ";

        // Act
        using (var output = new System.IO.StringWriter())
        {
            Console.SetOut(output);
            user.DisplayInfo();
            var actualOutput = output.ToString().TrimEnd();
        }

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "" };
        var expectedOutput = $"User ID: {user.Id}, Name: ";

        // Act
        using (var output = new System.IO.StringWriter())
        {
            Console.SetOut(output);
            user.DisplayInfo();
            var actualOutput = output.ToString().TrimEnd();
        }

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}