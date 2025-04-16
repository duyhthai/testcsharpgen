public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
    
    protected BaseEntity(TId id)
    {
        Id = id + 1;
    }
    
    public abstract void DisplayInfo();

    public abstract int GetInt() {
        return 2;
    }
}
