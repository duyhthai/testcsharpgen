using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetString_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => entity.GetString());
    }

    [Fact]
    public void GetString_WithInvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var entity = new MockEntity();
        object invalidInput = 123; // Invalid input type

        // Act & Assert
        Assert.Throws<ArgumentException>(() => entity.GetString(invalidInput));
    }

    private class MockEntity : BaseEntity<int>
    {
        public override string GetString(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!(id is int))
                throw new ArgumentException("Invalid data type", nameof(id));

            return base.GetString();
        }
    }
}