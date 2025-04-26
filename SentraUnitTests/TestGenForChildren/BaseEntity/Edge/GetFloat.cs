using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetFloat_ExtremePositiveValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetFloat());
    }

    [Fact]
    public void GetFloat_ExtremeNegativeValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetFloat());
    }

    [Fact]
    public void GetFloat_ZeroValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetFloat());
    }
}

public class MockEntity : BaseEntity<int>
{
    public override int GetFloat()
    {
        throw new InvalidOperationException("This method should never be called.");
    }
}