using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetFloat_ThrowsArgumentException_WhenInputIsNegative()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetFloat(-1));
    }

    [Test]
    public void GetFloat_ThrowsArgumentNullException_WhenInputIsNull()
    {
        // Arrange
        var userEntity = new UserEntity();
        object input = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.GetFloat(input));
    }

    [Test]
    public void GetFloat_ThrowsInvalidOperationException_WhenInputIsOutOfRange()
    {
        // Arrange
        var userEntity = new UserEntity();
        float input = float.MaxValue + 1;

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetFloat(input));
    }
}