using Gertec.Inventory.Management.Domain.Abstractions;

namespace Gertec.Inventory.Management.Domain.Entities;

public class Item : DefaultEntityBase
{
    public string PartNumber { get; set; }
    public string Description { get; set; }
}