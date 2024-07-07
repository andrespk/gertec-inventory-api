using System.Linq.Expressions;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IReadOperations<TEntity, TIdType, TModel>
{
    Task<TModel?> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken? cancellationToken);

    Task<IEnumerable<TModel>> GetManyAsync(Expression<Func<TEntity, bool>>? predicate,
        CancellationToken? cancellationToken);
}