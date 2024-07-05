using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Resources;

namespace Gertec.Inventory.Management.Domain.Validators;

public class DailyInventoryValidator : FluentValidatorBase<DailyInventory>
{
    private const int GreaterThanOrEqualsTo = 0;

    public DailyInventoryValidator()
    {
        RuleFor(x => x.Item).NotEmpty().WithMessage(FormatMessage(InventoryMessages.InvalidItemPartNumber,
            InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Date).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(InventoryMessages.InvalidDate);

        RuleFor(x => x.Balance.Quantity).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(
                FormatMessage(InventoryMessages.InvalidQuantity, InventoryConstants.ItemDefaultMinimumQuantity));

        RuleFor(x => x.Balance.Amount).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(InventoryMessages.InvalidAmount, InventoryConstants.ItemDefaultMinimumAmount));

        RuleFor(x => x.Balance.UnitPrice).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(InventoryMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
    }
}