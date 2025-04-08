public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
    
    protected BaseEntity(TId id)
    {
        Id = id;
    }
    
    public abstract void DisplayInfo();
}
