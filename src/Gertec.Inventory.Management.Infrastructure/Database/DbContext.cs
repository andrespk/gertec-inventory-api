using System.Data;
using Gertec.Inventory.Management.Domain.Abstractions;
using MySql.Data.MySqlClient;

namespace Gertec.Inventory.Management.Infrastructure.Database;

public class DbContext: IDbContext
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}