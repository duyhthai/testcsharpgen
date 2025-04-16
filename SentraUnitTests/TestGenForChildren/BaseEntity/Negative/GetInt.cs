using NUnit.Framework;

[TestFixture]
public class BaseEntityTests
{
    [Test]
    public void GetInt_ThrowsArgumentException_WhenInputIsInvalid()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.GetInt());
    }

    [Test]
    public void GetInt_ThrowsInvalidOperationException_WhenStateIsInvalid()
    {
        // Arrange
        var entity = new MockEntity();
        entity.SetInvalidState();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetInt());
    }

    [Test]
    public void GetInt_ThrowsNullReferenceException_WhenDependencyIsNull()
    {
        // Arrange
        var entity = new MockEntity();
        entity.SetDependencyToNull();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => entity.GetInt());
    }
}

public class MockEntity : BaseEntity<int>
{
    private bool _isInvalidState;
    private object _dependency;

    public void SetInvalidState()
    {
        _isInvalidState = true;
    }

    public void SetDependencyToNull()
    {
        _dependency = null;
    }

    public override int GetInt()
    {
        if (_isInvalidState)
        {
            throw new InvalidOperationException("Invalid state");
        }

        if (_dependency == null)
        {
            throw new NullReferenceException("Dependency is null");
        }

        return base.GetInt();
    }
}