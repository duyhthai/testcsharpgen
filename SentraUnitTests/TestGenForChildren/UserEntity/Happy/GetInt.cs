using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetInt_ReturnsExpectedValue()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetInt();

        // Assert
        Assert.AreEqual(3, result);
    }

    [Test]
    public void GetInt_CallsBaseImplementation()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetInt();

        // Assert
        Assert.IsTrue(result == 3);
    }

    [Test]
    public void GetInt_DoesNotThrowException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.DoesNotThrow(() => userEntity.GetInt());
    }
}