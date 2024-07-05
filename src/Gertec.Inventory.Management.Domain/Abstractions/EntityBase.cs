namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class EntityBase<TIdType>
{
    public TIdType Id { get; set; }
}