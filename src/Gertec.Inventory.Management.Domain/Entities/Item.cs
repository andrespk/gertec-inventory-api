using DeclarativeSql;
using DeclarativeSql.Annotations;
using Gertec.Inventory.Management.Domain.Abstractions;

namespace Gertec.Inventory.Management.Domain.Entities;

[Table(DbKind.MySql, "items")]
public class Item : DefaultEntityBase
{
    public string PartNumber { get; set; }
    public string Name { get; set; }

    public bool IsRemoved { get; set; }

    public DateTime? RemovedAtUtc { get; set; }
}