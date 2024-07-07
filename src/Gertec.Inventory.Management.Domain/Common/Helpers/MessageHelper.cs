namespace Gertec.Inventory.Management.Domain.Common.Helpers;

public static class MessageHelper
{
    public static string Format(string messageTemplate, object value)
    {
        return string.Format(messageTemplate, value);
    }
}