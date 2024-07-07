using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IDailyInventoryRepository : IReadOperations<DailyInventory, Guid, DailyInventoryDto>,
    ICreateOperations<AddDailyInventoryDto>,
    IUpdateOperations<UpdateDailyInventoryDto>
{
    Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId, CancellationToken? cancellationToken);
}