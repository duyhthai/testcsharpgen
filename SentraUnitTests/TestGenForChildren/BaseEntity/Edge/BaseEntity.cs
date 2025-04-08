using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithMinValueId_ShouldDisplayCorrectly()
    {
        // Arrange
        var minValueId = default(int);
        var entity = new DerivedEntity(minValueId);

        // Act
        entity.DisplayInfo();

        // Assert
        // Assuming DisplayInfo prints something related to Id
        Assert.Contains(minValueId.ToString(), Console.Out.ToString());
    }

    [Fact]
    public void DisplayInfo_WithMaxValueId_ShouldDisplayCorrectly()
    {
        // Arrange
        var maxValueId = int.MaxValue;
        var entity = new DerivedEntity(maxValueId);

        // Act
        entity.DisplayInfo();

        // Assert
        // Assuming DisplayInfo prints something related to Id
        Assert.Contains(maxValueId.ToString(), Console.Out.ToString());
    }
}

public class DerivedEntity : BaseEntity<int>
{
    public DerivedEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
    }
}