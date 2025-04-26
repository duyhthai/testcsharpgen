using NUnit.Framework;

[TestFixture]
public class BaseEntityTests
{
    [Test]
    public void GetFloat_InvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.GetFloat());
    }

    [Test]
    public void GetFloat_NegativeValue_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var entity = new MockEntity { FloatValue = -1 };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => entity.GetFloat());
    }

    [Test]
    public void GetFloat_NullReference_ThrowsNullReferenceException()
    {
        // Arrange
        var entity = new MockEntity { FloatValue = null };

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => entity.GetFloat());
    }
}

public class MockEntity : BaseEntity<int>
{
    public int? FloatValue { get; set; }

    public override int GetFloat()
    {
        if (FloatValue == null)
            throw new NullReferenceException("Float value cannot be null");

        if (FloatValue < 0)
            throw new ArgumentOutOfRangeException("Float value cannot be negative");

        if (typeof(int).IsAssignableFrom(typeof(float)))
            throw new ArgumentException("Invalid data type");

        return (int)FloatValue;
    }
}