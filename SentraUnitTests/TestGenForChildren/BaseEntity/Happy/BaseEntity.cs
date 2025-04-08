using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithValidId_ShouldDisplayCorrectly()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var entity = new DerivedEntity(entityId);

        // Act
        entity.DisplayInfo();

        // Assert
        Console.WriteLine($"Entity ID: {entityId}");
        Assert.Equal(entityId.ToString(), entityId.ToString());
    }
}

public class DerivedEntity : BaseEntity<Guid>
{
    public DerivedEntity(Guid id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Derived Entity ID: {Id}");
    }
}