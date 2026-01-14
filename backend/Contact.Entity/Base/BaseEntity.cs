namespace Contact.Entity;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>;
