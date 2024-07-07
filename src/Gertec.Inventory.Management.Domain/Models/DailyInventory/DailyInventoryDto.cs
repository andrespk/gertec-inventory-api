using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Models.DailyInventory;

public record DailyInventoryDto(Guid itemId, DateTime Date, Balance Balance);