using System.ComponentModel.DataAnnotations.Schema;

namespace Gertec.Inventory.Management.Infrastructure.Data.Helpers;

public static class DbHelper
{
    public static string DefaultConnectionConfigName = "DEFAULT_CONNECTION";

    public static string GetTableName<T>()
    {
        var tableAttribute = typeof(T)
            .GetCustomAttributes(typeof(TableAttribute), true)
            .FirstOrDefault() as TableAttribute;

        return tableAttribute?.Name ?? nameof(T);
    }
}