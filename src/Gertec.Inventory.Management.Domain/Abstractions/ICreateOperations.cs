using System.Data;

namespace Gertec.Inventory.Management.Domain.Abstractions;

public interface ICreateOperations<in TModel>
{
    Task AdOneAsync(TModel model, CancellationToken cancellationToken);
    Task AddManyAsync(IEnumerable<TModel> model, CancellationToken cancellationToken);
}