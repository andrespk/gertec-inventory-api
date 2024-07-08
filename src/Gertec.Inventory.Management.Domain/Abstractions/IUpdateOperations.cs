using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface IUpdateOperations<in TModel>
{
    Task UpdateOneAsync(TModel model, CancellationToken cancellationToken);
    Task UpdateManyAsync(IEnumerable<TModel> models, CancellationToken cancellationToken);
}