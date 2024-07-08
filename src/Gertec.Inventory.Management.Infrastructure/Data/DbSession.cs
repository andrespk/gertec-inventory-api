using System.Data;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Gertec.Inventory.Management.Infrastructure.Data.Helpers;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Gertec.Inventory.Management.Infrastructure.Data;

public sealed class DbSession : IDbSession
{
    public Guid Id = Guid.NewGuid();
    public IDbConnection Connection { get; }
    public IDbTransaction? Transaction { get; private set; }

    public DbSession(IConfiguration configuration) 
    {
        Connection = new MySqlConnection(configuration.GetConnectionString(DataHelper.DefaultConnectionConfigName));
        Connection.Open();
    }

    public void SetTransaction(IDbTransaction transaction) => Transaction = transaction;
   
    public void Dispose() => Connection?.Dispose();
}