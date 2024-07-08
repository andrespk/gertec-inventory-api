namespace Gertec.Inventory.Management.Domain.Models.DailyInventory;

public record DecreaseDailyInventoryDto(Guid ItemId, DateTime Date, decimal Quantity);