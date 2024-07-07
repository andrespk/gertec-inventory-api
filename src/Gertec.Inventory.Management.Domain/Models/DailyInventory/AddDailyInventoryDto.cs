namespace Gertec.Inventory.Management.Domain.Models.DailyInventory;

public record AddDailyInventoryDto(Guid itemId, DateTime Date, decimal Quantity, decimal Amount, decimal UnitPrice);