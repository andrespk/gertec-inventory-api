using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using MediatR;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Decrease;

public class DecreaseDailyInventoryCommand : IRequest<DailyInventoryDto>
{
    public Guid ItemId { get; init; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; init; }
}