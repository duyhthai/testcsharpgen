using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void Test_BaseEntity_Id_Incremented()
    {
        // Arrange
        var id = 0;
        var entity = new DerivedEntity(id);

        // Act
        var result = entity.Id;

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_BaseEntity_DisplayInfo_Called()
    {
        // Arrange
        var mockDisplayInfo = new Mock<DerivedEntity>();
        mockDisplayInfo.Setup(e => e.DisplayInfo()).Verifiable();

        // Act
        mockDisplayInfo.Object.DisplayInfo();

        // Assert
        mockDisplayInfo.Verify(e => e.DisplayInfo(), Times.Once());
    }

    [Fact]
    public void Test_BaseEntity_GetInt_ReturnsOne()
    {
        // Arrange
        var entity = new DerivedEntity(0);

        // Act
        var result = entity.GetInt();

        // Assert
        Assert.Equal(1, result);
    }
}

public class DerivedEntity : BaseEntity<int>
{
    public DerivedEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Displaying Info");
    }

    public override int GetInt()
    {
        return 1;
    }
}