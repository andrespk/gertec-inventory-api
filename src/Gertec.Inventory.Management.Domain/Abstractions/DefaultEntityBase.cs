namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultEntityBase : EntityBase<Guid>
{
    public DefaultEntityBase()
    {
        Id = Guid.NewGuid();
    }
}