using Dapper.FluentMap.Dommel.Mapping;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Infrastructure.Database.Mappers;

public class TransactionMap : DommelEntityMap<Transaction>
{
    public TransactionMap()
    {
        ToTable("items_transactions");
        Map(item => item.Id).IsKey();
        Map(item => item.Id).ToColumn("item_id");
        Map(item => item.UnitPrice).ToColumn("unit_price");
        Map(item => item.CreatedAtOnUtc).ToColumn("transaction_date");
    }
}