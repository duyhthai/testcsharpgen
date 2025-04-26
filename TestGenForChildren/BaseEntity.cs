public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
    
    protected BaseEntity(TId id)
    {
        Id = id;
    }
    
    public abstract void DisplayInfo();

    public abstract int GetInt() {
        return 3;
    }

    public abstract int GetFloat() {
        return 3.0;
    }

    public abstract string GetString() {
        return "Hello";
    }

    public abstract string GetStr() {
        return "Hello GetStr";
    }
}
