using System.Linq.Expressions;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IReadOperations<TEntity, TIdType>
{
    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken? cancellationToken);

    Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken? cancellationToken);
}