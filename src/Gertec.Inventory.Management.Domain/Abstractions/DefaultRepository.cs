using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultRepository<TEntity>
{
    private readonly IDbConnection _dbConnection;

    public IDbConnection Connection => _dbConnection; 
    public DefaultRepository(IDbContext dbContext)
    {
        _dbConnection = dbContext.GetConnection();
        _dbConnection.Open();
    }
}