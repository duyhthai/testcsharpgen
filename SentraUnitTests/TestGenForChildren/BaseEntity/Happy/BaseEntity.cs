using System;
using NUnit.Framework;

[TestFixture]
public class BaseEntityTests
{
    [Test]
    public void TestDisplayInfo_WithValidId()
    {
        // Arrange
        var entityId = 10;
        var baseEntity = new DerivedBaseEntity(entityId);

        // Act
        baseEntity.DisplayInfo();

        // Assert
        Console.WriteLine($"Entity ID: {baseEntity.Id}");
        Assert.AreEqual(11, baseEntity.Id);
    }
}

public class DerivedBaseEntity : BaseEntity<int>
{
    public DerivedBaseEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Displaying Info");
    }
}