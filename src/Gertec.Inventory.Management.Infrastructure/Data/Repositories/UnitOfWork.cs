using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    public IDbSession DbSession { get; init; }
    public void BeginTransaction() => DbSession.SetTransaction(DbSession.Connection.BeginTransaction());
    
    public UnitOfWork(IDbSession dbSession)
    {
        DbSession = dbSession;
    }
    public void Commit()
    {
        DbSession.Transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        DbSession.Transaction?.Rollback();
        Dispose();
    }

    public void Dispose() => DbSession.Transaction?.Dispose();
}