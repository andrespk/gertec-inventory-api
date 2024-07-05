using Gertec.Inventory.Management.Domain.Abstractions;

namespace Gertec.Inventory.Management.Domain.Exceptions;

public class ItemNotFoundException : DefaultException
{
    public ItemNotFoundException()
    {
        Message = "Item not found.";
    }
}