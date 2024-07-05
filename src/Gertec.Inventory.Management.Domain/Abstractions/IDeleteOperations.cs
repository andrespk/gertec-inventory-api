namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IDeleteOperations<in TEntity>
{
    Task DeleteOneAsync(TEntity entity, CancellationToken? cancellationToken);
    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken? cancellationToken);
    
}