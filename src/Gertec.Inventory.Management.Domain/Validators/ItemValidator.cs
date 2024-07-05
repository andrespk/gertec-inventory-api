using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Resources;

namespace Gertec.Inventory.Management.Domain.Validators;

public class ItemValidator : FluentValidatorBase<Item>
{
    public ItemValidator()
    {
        RuleFor(x => x.PartNumber).NotEmpty().Length(InventoryConstants.ItemPartNumberDefaultLength).WithMessage(
            FormatMessage(InventoryMessages.InvalidItemPartNumber,
                InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Description).NotEmpty().MaximumLength(InventoryConstants.ItemPartDescriptionDefaultMaxLength)
            .WithMessage(FormatMessage(InventoryMessages.InvalidItemDescriptionMaxLength,
                InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Description).NotEmpty().MinimumLength(InventoryConstants.ItemPartDescriptionDefaultMinLength)
            .WithMessage(FormatMessage(InventoryMessages.InvalidItemDescriptionMinLength,
                InventoryConstants.ItemPartDescriptionDefaultMinLength));
    }
}