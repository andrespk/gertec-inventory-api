using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using MediatR;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Increase;

public class IncreaseDailyInventoryCommand : IRequest<DailyInventoryDto>
{
    public Guid ItemId { get; init; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; init; }
    public decimal Amount { get; init; }
    public decimal UnitPrice { get; init; }

    public bool IsUnitPriceValid => Quantity != 0 && (Amount / Quantity) == UnitPrice;

    public IncreaseDailyInventoryCommand(Guid itemId, DateTime date, decimal quantity, decimal amount,
        decimal unitPrice)
    {
        ItemId = itemId;
        Date = date;
        Quantity = quantity;
        Amount = amount;
        UnitPrice = unitPrice;
    }
}