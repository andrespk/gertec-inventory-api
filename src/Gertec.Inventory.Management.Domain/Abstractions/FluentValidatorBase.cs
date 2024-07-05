using FluentValidation;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class FluentValidatorBase<TEntity> :  AbstractValidator<TEntity>
{
    protected string FormatMessage(string messageTemplate, object value) => string.Format(messageTemplate, value);
}