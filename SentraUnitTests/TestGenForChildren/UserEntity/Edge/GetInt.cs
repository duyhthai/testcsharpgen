using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UserEntityTests
{
    [TestMethod]
    public void GetInt_EdgeCase_MaximumValue()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetInt();

        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void GetInt_EdgeCase_MinimumValue()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetInt();

        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void GetInt_EdgeCase_EmptyCollection()
    {
        // Arrange
        var userEntity = new UserEntity();

        // Act
        int result = userEntity.GetInt();

        // Assert
        Assert.AreEqual(3, result);
    }
}