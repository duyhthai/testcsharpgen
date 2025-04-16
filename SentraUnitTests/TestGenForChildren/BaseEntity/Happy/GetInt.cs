using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetInt_Returns_3()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act
        int result = entity.GetInt();

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void GetInt_Returns_Correct_Value()
    {
        // Arrange
        var entity = new ConcreteEntity { Id = 1 };

        // Act
        int result = entity.GetInt();

        // Assert
        Assert.Equal(3, result);
    }

    private class ConcreteEntity : BaseEntity<int>
    {
        public override int GetInt()
        {
            return base.GetInt();
        }
    }
}