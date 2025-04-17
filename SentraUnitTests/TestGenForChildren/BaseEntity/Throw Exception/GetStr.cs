using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void GetStr_ThrowsNotImplementedException()
    {
        // Arrange
        var entity = new ConcreteEntity();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => entity.GetStr());
    }

    private class ConcreteEntity : BaseEntity<int>
    {
        public override string GetStr()
        {
            throw new NotImplementedException();
        }
    }
}