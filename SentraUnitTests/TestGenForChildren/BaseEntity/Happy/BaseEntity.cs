using System;
using Xunit;

public class BaseEntityTests
{
    [Fact]
    public void HappyTest_BaseEntity_CreatesInstanceWithIncrementedId()
    {
        // Arrange
        var id = 5;
        
        // Act
        var entity = new DerivedBaseEntity(id);
        
        // Assert
        Assert.Equal(6, entity.Id);
    }
    
    [Fact]
    public void HappyTest_BaseEntity_DisplayInfo_CallsDisplayMethod()
    {
        // Arrange
        var mockDisplay = new Mock<IDisplay>();
        var entity = new DerivedBaseEntity(0)
        {
            DisplayInfo = () => mockDisplay.Object.Display()
        };
        
        // Act
        entity.DisplayInfo();
        
        // Assert
        mockDisplay.Verify(d => d.Display(), Times.Once());
    }
}

public class DerivedBaseEntity : BaseEntity<int>
{
    public Action DisplayInfo { get; set; }

    public DerivedBaseEntity(int id) : base(id)
    {
    }

    public override void DisplayInfo()
    {
        if (DisplayInfo != null)
        {
            DisplayInfo();
        }
    }
}

public interface IDisplay
{
    void Display();
}