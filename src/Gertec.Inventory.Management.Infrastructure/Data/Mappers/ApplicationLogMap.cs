using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Data.Mappers;

public class ApplicationLogMap : DommelEntityMap<ApplicationLog>
{
    public ApplicationLogMap()
    {
        ToTable("items_daily_inventory");
        Map(item => item.Id).IsKey();
    }
}