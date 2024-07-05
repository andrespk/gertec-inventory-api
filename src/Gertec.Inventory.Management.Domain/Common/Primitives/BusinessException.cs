namespace Gertec.Inventory.Management.Domain.Common.Primitives;

public record BusinessException(string Code, string Description)
{
    public static readonly BusinessException None = new(string.Empty, string.Empty);
}