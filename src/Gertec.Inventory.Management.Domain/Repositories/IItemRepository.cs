using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IItemRepository : IReadOperations<Item, Guid>, ICreateOperations<Item>,
    IUpdateOperations<Item>
{
    
}