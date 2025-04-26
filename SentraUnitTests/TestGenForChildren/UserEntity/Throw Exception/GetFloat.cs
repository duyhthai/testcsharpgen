using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetFloat_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetFloat());
    }
}