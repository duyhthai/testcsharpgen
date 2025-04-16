using NUnit.Framework;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void GetInt_WithInvalidDataType_ThrowsArgumentException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => userEntity.GetInt());
    }

    [Test]
    public void GetInt_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => userEntity.GetInt());
    }

    [Test]
    public void GetInt_WithEmptyString_ThrowsInvalidOperationException()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => userEntity.GetInt());
    }
}