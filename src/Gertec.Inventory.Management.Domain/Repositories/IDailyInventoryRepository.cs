using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IDailyInventoryRepository : IReadOperations<DailyInventory, Guid>, ICreateOperations<DailyInventory>,
    IUpdateOperations<DailyInventory>;