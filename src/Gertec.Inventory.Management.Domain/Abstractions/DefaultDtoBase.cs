using Mapster;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultDtoBase
{
    public T To<T>()
    {
        return this.Adapt<T>();
    }

    public abstract void From<T>(T source);
}