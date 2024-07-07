using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultRepository : IDisposable
{
    private readonly IDbConnection _dbConnection;

    public IDbConnection Connection => _dbConnection; 
    public DefaultRepository(IDbContext dbContext)
    {
        _dbConnection = dbContext.GetConnection();
        _dbConnection.Open();
    }

    public CancellationToken ResolveAndConfigureCancellationToken(CancellationToken? cancellationToken)
    {
        var token = cancellationToken ?? new ();
        token.ThrowIfCancellationRequested();
        return token;
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}