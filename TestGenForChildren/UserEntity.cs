public class UserEntity : BaseEntity<int>
{
    public string Name { get; set; }
    
    public UserEntity(int id, string name) : base(id)
    {
        Name = name;
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"User ID: {Id}, Name: {Name}");
    }
}