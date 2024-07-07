using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common.Resources;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Validators;

public class ItemValidator : ValidatorBase<Item>
{
    public ItemValidator()
    {
        RuleFor(x => x.PartNumber).NotEmpty().Length(InventoryConstants.ItemPartNumberDefaultLength).WithMessage(
            FormatMessage(BusinessMessages.InvalidItemPartNumber,
                InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Name).NotEmpty().MaximumLength(InventoryConstants.ItemPartDescriptionDefaultMaxLength)
            .WithMessage(FormatMessage(BusinessMessages.InvalidItemDescriptionMaxLength,
                InventoryConstants.ItemPartNumberDefaultLength));

        RuleFor(x => x.Name).NotEmpty().MinimumLength(InventoryConstants.ItemPartDescriptionDefaultMinLength)
            .WithMessage(FormatMessage(BusinessMessages.InvalidItemDescriptionMinLength,
                InventoryConstants.ItemPartDescriptionDefaultMinLength));
    }
}