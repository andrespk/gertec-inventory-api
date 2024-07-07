using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultRepository : IDisposable
{
    public DefaultRepository(IDbContext dbContext)
    {
        Connection = dbContext.GetConnection();
        Connection.Open();
    }

    public IDbConnection Connection { get; }

    public void Dispose()
    {
        Connection.Dispose();
    }

    public CancellationToken ResolveAndConfigureCancellationToken(CancellationToken? cancellationToken)
    {
        var token = cancellationToken ?? new CancellationToken();
        token.ThrowIfCancellationRequested();
        return token;
    }
}