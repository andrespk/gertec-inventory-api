using Gertec.Inventory.Management.Infrastructure.Database.Abstractions;
using MySql.Data.MySqlClient;

namespace Gertec.Inventory.Management.Infrastructure.Database;

public class InventoryDbContext: IDapperDbContext<MySqlConnection>
{
    private readonly string _connectionString;

    public InventoryDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}