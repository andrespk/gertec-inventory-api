using System.Data;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IDailyInventoryRepository : IReadOperations<DailyInventory, Guid, DailyInventoryDto>
{
    Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId, CancellationToken? cancellationToken);
    
    Task<DailyInventoryDto?> GetByItemIdAndDateAsync(Guid itemId, DateTime date, CancellationToken? cancellationToken);

    Task IncreaseAsync(IncreaseDailyInventoryDto model, CancellationToken? cancellationToken,
        IDbTransaction? transaction);

    Task DecreaseAsync(DecreaseDailyInventoryDto model, CancellationToken? cancellationToken,
        IDbTransaction? transaction);
}