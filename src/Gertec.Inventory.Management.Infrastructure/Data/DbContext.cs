using System.Data;
using Gertec.Inventory.Management.Domain.Abstractions;
using MySql.Data.MySqlClient;

namespace Gertec.Inventory.Management.Infrastructure.Data;

public sealed class DbContext : IDbContext
{
    private readonly string _connectionString;

    public DbContext(string connectionString) => _connectionString = connectionString;

    public IDbConnection GetConnection() => new MySqlConnection(_connectionString);
}