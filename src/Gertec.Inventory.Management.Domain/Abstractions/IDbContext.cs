using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IDbContext
{
    public IDbConnection GetConnection();
}