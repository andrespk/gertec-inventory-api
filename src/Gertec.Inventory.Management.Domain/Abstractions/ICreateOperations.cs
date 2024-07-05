using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface ICreateOperations<in TEntity>
{
    Task AdOneAsync(TEntity entity, IDbTransaction? transaction, CancellationToken? cancellationToken);
    Task AddManyAsync(IEnumerable<TEntity> entities, IDbTransaction? transaction, CancellationToken? cancellationToken);
}