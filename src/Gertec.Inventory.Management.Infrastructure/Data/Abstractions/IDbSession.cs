using System.Data;

namespace Gertec.Inventory.Management.Infrastructure.Data.Abstractions;

public interface IDbSession: IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }
    void SetTransaction(IDbTransaction transaction);
}