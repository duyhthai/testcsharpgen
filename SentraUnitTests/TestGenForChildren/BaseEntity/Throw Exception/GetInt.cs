using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetInt_ThrowsNotImplementedException()
    {
        // Arrange
        var entity = new MockBaseEntity();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => entity.GetInt());
    }

    private class MockBaseEntity : BaseEntity<int>
    {
        public override int GetInt()
        {
            throw new NotImplementedException();
        }
    }
}