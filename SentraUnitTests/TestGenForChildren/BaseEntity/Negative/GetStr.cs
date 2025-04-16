using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetStr_WithInvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ((dynamic)entity).GetStr());
    }

    [Fact]
    public void GetStr_WithNullReference_ThrowsInvalidOperationException()
    {
        // Arrange
        BaseEntity<int> entity = null;

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => entity.GetStr());
    }

    [Fact]
    public void GetStr_WithEmptyString_ThrowsArgumentNullException()
    {
        // Arrange
        var entity = new MockEntity { Id = "" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => entity.GetStr());
    }
}

public class MockEntity : BaseEntity<string>
{
    public override string GetStr()
    {
        return base.GetStr();
    }
}