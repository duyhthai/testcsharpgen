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

    [Fact]
    public void GetInt_ThrowsArgumentException_WhenInputIsInvalid()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.SetInt(-1));
    }

    [Fact]
    public void GetInt_ThrowsInvalidOperationException_WhenStateIsInvalid()
    {
        // Arrange
        var entity = new ConcreteEntity();
        entity.SetState(false);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetInt());
    }

    [Fact]
    public void GetInt_ThrowsNullReferenceException_WhenDependencyIsNull()
    {
        // Arrange
        var entity = new ConcreteEntity();
        entity.SetDependency(null);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => entity.GetInt());
    }
}

public class ConcreteEntity : BaseEntity<int>
{
    private bool _isValidState;
    private object _dependency;

    public override int GetInt()
    {
        if (!_isValidState)
            throw new InvalidOperationException("Invalid state");

        if (_dependency == null)
            throw new NullReferenceException("Dependency is null");

        return 2;
    }

    public void SetInt(int value)
    {
        if (value < 0)
            throw new ArgumentException("Value cannot be negative");
    }

    public void SetState(bool isValidState)
    {
        _isValidState = isValidState;
    }

    public void SetDependency(object dependency)
    {
        _dependency = dependency;
    }
}