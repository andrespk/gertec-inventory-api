using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common.Resources;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Validators;

public class TransactionValidator : FluentValidatorBase<Transaction>
{
    private const int GreaterThanOrEqualsTo = 0;
    
    public TransactionValidator()
    {
        RuleFor(x => x.Item).NotEmpty().WithMessage(FormatMessage(BusinessMessages.InvalidItemPartNumber,
            InventoryConstants.ItemPartNumberDefaultLength));
        
        RuleFor(x => x.Type).NotEmpty().
            WithMessage(BusinessMessages.InvalidTransactionType);
        
        RuleFor(x => x.CreatedAtOnUtc).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(BusinessMessages.InvalidDate);

        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(
                FormatMessage(BusinessMessages.InvalidQuantity, InventoryConstants.ItemDefaultMinimumQuantity));

        RuleFor(x => x.Amount).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(BusinessMessages.InvalidAmount, InventoryConstants.ItemDefaultMinimumAmount));

        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(GreaterThanOrEqualsTo)
            .WithMessage(FormatMessage(BusinessMessages.InvalidUnitPrice,
                InventoryConstants.ItemDefaultMinimumUnitPrice));
    }
}