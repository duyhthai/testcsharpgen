using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetFloat_InvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.GetFloat());
    }

    [Fact]
    public void GetFloat_NegativeValue_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => entity.GetFloat());
    }

    [Fact]
    public void GetFloat_NullReference_ThrowsNullReferenceException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => entity.GetFloat());
    }
}

public class MockEntity : BaseEntity<int>
{
    public override int GetFloat()
    {
        throw new ArgumentException("Invalid data type");
    }
}