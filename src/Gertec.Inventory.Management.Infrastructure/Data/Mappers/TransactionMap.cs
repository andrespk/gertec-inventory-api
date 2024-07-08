using System.Diagnostics.CodeAnalysis;
using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Data.Mappers;

[ExcludeFromCodeCoverage]
public class TransactionMap : DommelEntityMap<Transaction>
{
    public TransactionMap()
    {
        ToTable("items_transactions");
        Map(x => x.Item.Id).ToColumn("item_id");
        Map(x => x.Type).ToColumn("transaction_type");
        Map(x => x.UnitPrice).ToColumn("unit_price");
        Map(x => x.InventoriedAtUtc).ToColumn("inventory_date");
        Map(x => x.CreatedAtUtc).ToColumn("transaction_date");
    }
}