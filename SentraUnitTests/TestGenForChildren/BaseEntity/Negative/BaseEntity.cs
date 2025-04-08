using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithNullId_ShouldThrowArgumentNullException()
    {
        // Arrange
        var entity = new DerivedEntity(null);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => entity.DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_WithEmptyStringId_ShouldThrowArgumentException()
    {
        // Arrange
        var entity = new DerivedEntity("");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.DisplayInfo());
    }
}

public class DerivedEntity : BaseEntity<string>
{
    public DerivedEntity(string id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
    }
}