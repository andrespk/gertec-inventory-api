using FluentValidation;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Validators;

public class ItemValidator : AbstractValidator<Item>
{
    private const int PartNumberLength = 10;
    private const int DescriptionMaxLength = 255;
    private const int DescriptionMinLength = 3;

    public ItemValidator()
    {
        RuleFor(x => x.PartNumber).NotEmpty().Length(PartNumberLength)
            .WithMessage($"A Part Number with {PartNumberLength} character(s) is required.");

        RuleFor(x => x.Description).NotEmpty().MaximumLength(DescriptionMaxLength)
            .WithMessage($"A Description with {DescriptionMaxLength} maximum character(s) is required.");

        RuleFor(x => x.Description).NotEmpty().MinimumLength(DescriptionMinLength)
            .WithMessage($"A Description with {DescriptionMinLength} minimum character(s) is required.");
    }
}