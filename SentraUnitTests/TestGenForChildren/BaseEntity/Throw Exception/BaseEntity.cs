using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void DisplayInfo_WithNullId_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullId = default(int?);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestEntity(nullId).DisplayInfo());
    }

    [Fact]
    public void DisplayInfo_WithInvalidId_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidId = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TestEntity(invalidId).DisplayInfo());
    }
}

public class TestEntity : BaseEntity<int?>
{
    public TestEntity(int? id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        // Implementation not required for this test
    }
}