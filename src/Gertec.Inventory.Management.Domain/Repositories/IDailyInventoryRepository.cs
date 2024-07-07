using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IDailyInventoryRepository : IReadOperations<DailyInventory, Guid>, ICreateOperations<DailyInventory>,
    IUpdateOperations<DailyInventory>
{
    Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId, CancellationToken? cancellationToken);
}