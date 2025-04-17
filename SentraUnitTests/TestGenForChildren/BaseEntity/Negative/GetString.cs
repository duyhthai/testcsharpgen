using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetString_WithInvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ((dynamic)entity).GetString());
    }

    [Fact]
    public void GetString_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var entity = new MockEntity { Id = null };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => entity.GetString());
    }

    private class MockEntity : BaseEntity<string>
    {
        public override string GetString()
        {
            return base.GetString();
        }
    }
}