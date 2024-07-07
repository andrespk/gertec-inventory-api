using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Items;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IItemRepository : IReadOperations<Item, Guid, ItemDto>, ICreateOperations<AddItemDto>,
    IUpdateOperations<UpdateItemDto>, IDeleteOperations<Guid>;