using System;
using Xunit;

public class UserEntityTests
{
    [Fact]
    public void DisplayInfo_ShouldPrintUserIdAndName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "John Doe" };
        var output = string.Empty;
        Console.SetOut(new System.IO.StringWriter(output));

        // Act
        user.DisplayInfo();

        // Assert
        output = Console.Out.ToString().Trim();
        Assert.Equal("User ID: 1, Name: John Doe", output);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleEmptyName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = "" };
        var output = string.Empty;
        Console.SetOut(new System.IO.StringWriter(output));

        // Act
        user.DisplayInfo();

        // Assert
        output = Console.Out.ToString().Trim();
        Assert.Equal("User ID: 1, Name:", output);
    }

    [Fact]
    public void DisplayInfo_ShouldHandleNullName()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Name = null };
        var output = string.Empty;
        Console.SetOut(new System.IO.StringWriter(output));

        // Act
        user.DisplayInfo();

        // Assert
        output = Console.Out.ToString().Trim();
        Assert.Equal("User ID: 1, Name:", output);
    }
}