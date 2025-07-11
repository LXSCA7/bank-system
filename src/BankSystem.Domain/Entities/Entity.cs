namespace BankSystem.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; }  = DateTime.Now;
    public bool Active { get; private set; } = true;
    public DateTime? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public void Deactivate(Guid deletedBy)
    {
        Active = false;
        DeletedAt = DateTime.Now;
        DeletedBy = deletedBy;
    }
}