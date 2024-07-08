using System.Diagnostics.CodeAnalysis;
using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Data.Mappers;

[ExcludeFromCodeCoverage]
public class ItemMap : DommelEntityMap<Item>
{
    public ItemMap()
    {
        ToTable("items");
        Map(x => x.PartNumber).ToColumn("part_no");
    }
}