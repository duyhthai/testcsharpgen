using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        var invalidId = default(int);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new InvalidEntity(invalidId).DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_WithNullId_ThrowsArgumentNullException()
    {
        // Arrange
        object nullId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new InvalidEntity(nullId).DisplayInfo());
    }
}

public class InvalidEntity : BaseEntity<int>
{
    public InvalidEntity(object id)
        : base((int?)id ?? throw new ArgumentNullException(nameof(id)))
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
    }
}