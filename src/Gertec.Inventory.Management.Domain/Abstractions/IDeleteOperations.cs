using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IDeleteOperations<in TIdType>
{
    Task DeleteOneAsync(TIdType id, CancellationToken cancellationToken);
    Task DeleteManyAsync(IEnumerable<TIdType> ids, CancellationToken cancellationToken);
}