using System.Data;
using Gertec.Inventory.Management.Domain.Common.Resources;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public abstract class DefaultRepository : IDisposable
{

    private readonly IDbSession _dbSession; 
    
    public IDbConnection Connection { get; }
    public DefaultRepository(IDbSession dbSession)
    {
        if (dbSession.Connection.State != ConnectionState.Open)
            throw new ArgumentException(BusinessMessages.InvalidConnection);
        
        Connection = dbSession.Connection;
    }

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

