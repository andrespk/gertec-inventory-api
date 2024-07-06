using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Database.Mappers;

public class DailyInventoryMap : DommelEntityMap<DailyInventory>
{
    public DailyInventoryMap()
    {
        ToTable("items_daily_inventory");
        Map(item => item.Id).IsKey();
        Map(item => item.Item).ToColumn("item_id");
        Map(item => item.Date).ToColumn("inventory_date");
        Map(item => item.Balance.Quantity).ToColumn("quantity");
        Map(item => item.Balance.Amount).ToColumn("amount");
        Map(item => item.Balance.UnitPrice).ToColumn("avg_unit_price");
        Map(item => item.Transactions).Ignore();
    }
}