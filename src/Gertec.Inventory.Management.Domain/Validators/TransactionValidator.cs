using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common.Resources;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Validators;

public class TransactionValidator : FluentValidatorBase<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Item).SetValidator(new ItemValidator());

        RuleFor(x => x.Type).NotEmpty().WithMessage(BusinessMessages.InvalidTransactionType);

        RuleFor(x => x.InventoriedAtUtc).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(BusinessMessages.InvalidDate);

        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(InventoryConstants.ItemDefaultMinimumQuantity)
            .WithMessage(
                FormatMessage(BusinessMessages.InvalidQuantity, InventoryConstants.ItemDefaultMinimumQuantity));

        RuleFor(x => x.Amount).GreaterThanOrEqualTo(InventoryConstants.ItemDefaultMinimumAmount)
            .WithMessage(FormatMessage(BusinessMessages.InvalidAmount, InventoryConstants.ItemDefaultMinimumAmount));

        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(InventoryConstants.ItemDefaultMinimumUnitPrice)
            .WithMessage(FormatMessage(BusinessMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
    }
}