using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void EdgeTest_BaseEntity_CreatesInstanceWithMinimumId()
    {
        // Arrange
        var minimumId = int.MinValue;
        
        // Act
        var entity = new DerivedEntity(minimumId);
        
        // Assert
        Assert.Equal(int.MinValue + 1, entity.Id);
    }

    [Fact]
    public void EdgeTest_BaseEntity_CreatesInstanceWithMaximumId()
    {
        // Arrange
        var maximumId = int.MaxValue;
        
        // Act
        var entity = new DerivedEntity(maximumId);
        
        // Assert
        Assert.Equal(int.MaxValue + 1, entity.Id);
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