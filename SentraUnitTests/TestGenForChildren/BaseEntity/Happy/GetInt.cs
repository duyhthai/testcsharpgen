using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetInt_ReturnsCorrectValue()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        int result = entity.GetInt();

        // Assert
        Assert.Equal(2, result);
    }

    private class ConcreteEntity : BaseEntity<int>
    {
        public override int GetInt()
        {
            return base.GetInt();
        }
    }
}