namespace Gertec.Inventory.Management.Domain.Models.DailyInventory;

public record IncreaseDailyInventoryDto(Guid ItemId, DateTime Date, decimal Quantity, decimal Amount, decimal UnitPrice);