namespace Gertec.Inventory.Management.Domain.Common.Extensions;

public static class NumericExtensions
{
    public static decimal TruncateTo(this decimal value, int precision)
    {
        var factor = int.Parse($"1{new string('0', precision)}");
        return Math.Truncate(factor * value) / factor;
    }
}