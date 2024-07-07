using System.Diagnostics.CodeAnalysis;
using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Database.Mappers;

[ExcludeFromCodeCoverage]
public class ItemMap : DommelEntityMap<Item>
{
    public ItemMap()
    {
        ToTable("items");
        Map(item => item.Id).IsKey();
        Map(item => item.PartNumber).ToColumn("part_no");
    }
}