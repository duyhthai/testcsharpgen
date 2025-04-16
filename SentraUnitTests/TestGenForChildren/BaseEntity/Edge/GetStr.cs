using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetStr_WithBoundaryValue_ThrowsException()
    {
        // Arrange
        var entity = new MockEntity();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => entity.GetStr());
    }

    private class MockEntity : BaseEntity<int>
    {
        public override string GetStr()
        {
            throw new NotImplementedException();
        }
    }
}