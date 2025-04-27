using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_Sets_Id_Correctly()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        
        // Act
        var entity = new BaseEntity(expectedId);
        
        // Assert
        Assert.Equal(expectedId, entity.Id);
    }

    [Fact]
    public void Constructor_With_Null_Id_Throws_ArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BaseEntity(default(Guid)));
    }

    [Fact]
    public void Constructor_With_Empty_String_Id_Throws_ArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new BaseEntity(""));
    }
}