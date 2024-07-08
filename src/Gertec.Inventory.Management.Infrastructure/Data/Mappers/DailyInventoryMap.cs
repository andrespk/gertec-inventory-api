using System.Diagnostics.CodeAnalysis;
using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Data.Mappers;

[ExcludeFromCodeCoverage]
public class DailyInventoryMap : DommelEntityMap<DailyInventory>
{
    public DailyInventoryMap()
    {
        ToTable("xs_daily_inventory");
        Map(x => x.Item.Id).ToColumn("item_id");
        Map(x => x.Date).ToColumn("inventory_date");
        Map(x => x.Balance.Quantity).ToColumn("quantity");
        Map(x => x.Balance.Amount).ToColumn("amount");
        Map(x => x.Balance.UnitPrice).ToColumn("avg_unit_price");
        Map(x => x.Transactions).Ignore();
    }
}