using FluentValidation;
using Gertec.Inventory.Management.Domain.Common.Helpers;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class FluentValidatorBase<TEntity> :  AbstractValidator<TEntity>
{
    protected string FormatMessage(string messageTemplate, object value) =>
        MessageHelper.Format(messageTemplate, value);
}