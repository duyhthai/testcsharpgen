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

    private class MockEntity : BaseEntity<int>
    {
        public override int GetInt()
        {
            return base.GetInt();
        }

        public void SetInvalidState()
        {
            // Simulate an invalid state
        }
    }
}