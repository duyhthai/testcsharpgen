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
    public void DisplayInfo_ShouldIncludeUserIdAndName()
    {
        // Arrange
        var user = new UserEntity { Id = 2, Name = "Jane Smith" };

        // Act
        using (var output = new System.IO.StringWriter())
        {
            Console.SetOut(output);
            user.DisplayInfo();
            var actualOutput = output.ToString().TrimEnd();
        }

        // Assert
        Assert.Contains("User ID: 2", actualOutput);
        Assert.Contains("Name: Jane Smith", actualOutput);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 3, Name = "" };

        // Act
        using (var output = new System.IO.StringWriter())
        {
            Console.SetOut(output);
            user.DisplayInfo();
            var actualOutput = output.ToString().TrimEnd();
        }

        // Assert
        Assert.Contains("User ID: 3", actualOutput);
        Assert.Contains("Name: ", actualOutput);
    }
}