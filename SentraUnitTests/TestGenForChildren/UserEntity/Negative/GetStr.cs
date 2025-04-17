using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetStr_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.Object.GetStr());
    }

    [Test]
    public void GetStr_WithEmptyStringInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.Object.GetStr(""));
    }

    [Test]
    public void GetStr_WithWhitespaceInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.Object.GetStr(" "));
    }
}