using FluentValidation;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class ValidatorBase<TEntity> : AbstractValidator<TEntity>
{
    protected string FormatMessage(string messageTemplate, object value)
    {
        return string.Format(messageTemplate, value);
    }
}