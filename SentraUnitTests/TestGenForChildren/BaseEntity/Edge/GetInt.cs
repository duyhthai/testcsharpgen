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
    public void GetInt_With_Maximum_Id_Returns_3()
    {
        // Arrange
        var entity = new ConcreteEntity { Id = int.MaxValue };

        // Act
        int result = entity.GetInt();

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void GetInt_With_Minimum_Id_Returns_3()
    {
        // Arrange
        var entity = new ConcreteEntity { Id = int.MinValue };

        // Act
        int result = entity.GetInt();

        // Assert
        Assert.Equal(3, result);
    }
}

public class ConcreteEntity : BaseEntity<int>
{
    public override int GetInt()
    {
        return base.GetInt();
    }
}