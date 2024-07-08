namespace Gertec.Inventory.Management.Infrastructure.Data.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IDbSession DbSession { get; init; }
    void BeginTransaction();
    void Commit();
    void Rollback();
}