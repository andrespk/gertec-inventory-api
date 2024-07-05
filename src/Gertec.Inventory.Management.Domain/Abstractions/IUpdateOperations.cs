using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IUpdateOperations<in TEntity>
{
    Task UpdateOneAsync(TEntity entity, IDbTransaction? transaction, CancellationToken? cancellationToken);
    Task UpdateManyAsync(IEnumerable<TEntity> entities, IDbTransaction? transaction, CancellationToken? cancellationToken);
}