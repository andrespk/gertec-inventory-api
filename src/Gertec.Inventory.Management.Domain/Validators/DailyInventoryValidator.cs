using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common;
using Gertec.Inventory.Management.Domain.Common.Resources;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Validators;

public class DailyInventoryValidator : FluentValidatorBase<DailyInventory>
{
    private const int GreaterThanOrEqualsTo = 0;

    public DailyInventoryValidator()
    {
        RuleFor(x => x.Item).NotEmpty().WithMessage(FormatMessage(BusinessMessages.InvalidItemPartNumber,
            InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Date).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(BusinessMessages.InvalidDate);

        RuleFor(x => x.Balance.Quantity).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(
                FormatMessage(BusinessMessages.InvalidQuantity, InventoryConstants.ItemDefaultMinimumQuantity));

        RuleFor(x => x.Balance.Amount).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(BusinessMessages.InvalidAmount, InventoryConstants.ItemDefaultMinimumAmount));

        RuleFor(x => x.Balance.UnitPrice).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(BusinessMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
    }
}