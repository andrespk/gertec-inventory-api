using DeclarativeSql;
using DeclarativeSql.Annotations;
using Gertec.Inventory.Management.Domain.Abstractions;

namespace Gertec.Inventory.Management.Domain.Entities;

[Table(DbKind.MySql, "items")]
public class Item : DefaultEntityBase
{
    public string PartNumber { get; set; }
    public string Description { get; set; }
}