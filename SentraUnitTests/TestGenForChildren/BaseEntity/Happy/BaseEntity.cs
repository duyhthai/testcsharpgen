using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Test_BaseEntity_Id_Incremented()
    {
        // Arrange
        var entityId = 10;
        var baseEntity = new DerivedBaseEntity(entityId);

        // Act
        var result = baseEntity.Id;

        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public void Test_BaseEntity_DisplayInfo_Called()
    {
        // Arrange
        var mockDisplayInfo = new Mock<DerivedBaseEntity>();
        mockDisplayInfo.Setup(b => b.DisplayInfo()).Verifiable();

        // Act
        mockDisplayInfo.Object.DisplayInfo();

        // Assert
        mockDisplayInfo.Verify(b => b.DisplayInfo(), Times.Once());
    }

    [Fact]
    public void Test_BaseEntity_GetInt_ReturnsOne()
    {
        // Arrange
        var baseEntity = new DerivedBaseEntity(0);

        // Act
        var result = baseEntity.GetInt();

        // Assert
        Assert.Equal(1, result);
    }
}

public class DerivedBaseEntity : BaseEntity<int>
{
    public DerivedBaseEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Displaying Info");
    }
}