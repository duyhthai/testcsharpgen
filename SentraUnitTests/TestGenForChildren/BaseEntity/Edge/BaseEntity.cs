using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void BaseEntity_WithMinValueId_ShouldIncrementIdByOne()
    {
        // Arrange
        var minValueId = int.MinValue;
        var expectedId = minValueId + 1;

        // Act
        var entity = new DerivedEntity(minValueId);

        // Assert
        Assert.Equal(expectedId, entity.Id);
    }

    [Fact]
    public void BaseEntity_WithMaxValueId_ShouldIncrementIdByOne()
    {
        // Arrange
        var maxValueId = int.MaxValue;
        var expectedId = maxValueId + 1;

        // Act
        var entity = new DerivedEntity(maxValueId);

        // Assert
        Assert.Equal(expectedId, entity.Id);
    }
}

public class DerivedEntity : BaseEntity<int>
{
    public DerivedEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        // Implementation not required for this test
    }
}