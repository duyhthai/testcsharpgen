using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetFloat_ReturnsExpectedValue()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetFloat();

        // Assert
        Assert.AreEqual(3, result);
    }

    [Test]
    public void GetFloat_CallsMethodAndReturnsInt()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetFloat();

        // Assert
        Assert.IsInstanceOf<int>(result);
    }

    [Test]
    public void GetFloat_DoesNotThrowException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.DoesNotThrow(() => userEntity.GetFloat());
    }
}