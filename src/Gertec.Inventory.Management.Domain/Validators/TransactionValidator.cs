using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Resources;

namespace Gertec.Inventory.Management.Domain.Validators;

public class TransactionValidator : FluentValidatorBase<Transaction>
{
    private const int GreaterThanOrEqualsTo = 0;
    
    public TransactionValidator()
    {
        RuleFor(x => x.Item).NotEmpty().WithMessage(FormatMessage(InventoryMessages.InvalidItemPartNumber,
            InventoryConstants.ItemPartNumberDefaultLength));
        
        RuleFor(x => x.Type).NotEmpty().
            WithMessage(InventoryMessages.InvalidTransactionType);
        
        RuleFor(x => x.CreatedAtOnUtc).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(InventoryMessages.InvalidDate);

        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(
                FormatMessage(InventoryMessages.InvalidQuantity, InventoryConstants.ItemDefaultMinimumQuantity));

        RuleFor(x => x.Amount).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(InventoryMessages.InvalidAmount, InventoryConstants.ItemDefaultMinimumAmount));

        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(InventoryMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
    }
}