using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetString_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.Object.GetString(null));
    }

    [Test]
    public void GetString_WithEmptyStringInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.Object.GetString(""));
    }

    [Test]
    public void GetString_WithWhitespaceInput_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new Mock<UserEntity>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.Object.GetString(" "));
    }
}