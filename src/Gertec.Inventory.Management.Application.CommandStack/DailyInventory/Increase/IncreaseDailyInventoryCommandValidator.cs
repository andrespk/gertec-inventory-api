using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common;
using Gertec.Inventory.Management.Domain.Common.Resources;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Increase;

public class IncreaseDailyInventoryCommandValidator : ValidatorBase<IncreaseDailyInventoryCommand>
{
    public IncreaseDailyInventoryCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotNull()
            .WithMessage(BusinessMessages.ItemNotFound);
        
        RuleFor(x => x.Date)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(BusinessMessages.InvalidDate);
        
        RuleFor(x => x.Quantity)
            .GreaterThan(InventoryConstants.ItemDefaultMinimumQuantity)
            .WithMessage(FormatMessage(BusinessMessages.InvalidQuantity,
                InventoryConstants.ItemDefaultMinimumQuantity));
        
        RuleFor(x => x.Amount)
            .GreaterThan(InventoryConstants.ItemDefaultMinimumAmount)
            .WithMessage(FormatMessage(BusinessMessages.InvalidAmount,
                InventoryConstants.ItemDefaultMinimumAmount));
        
        RuleFor(x => x.UnitPrice)
            .GreaterThan(InventoryConstants.ItemDefaultMinimumUnitPrice)
            .WithMessage(FormatMessage(BusinessMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
        
        RuleFor(x => x.IsUnitPriceValid)
            .Equal(false)
            .WithMessage(BusinessMessages.UnitPriceMismatch);
    }
}