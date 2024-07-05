using System.Data;
using System.Linq.Expressions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class DailyInventoryRepository : IDailyInventoryRepository
{
    public Task<DailyInventory> GetOneAsync(Expression<Func<DailyInventory, bool>>? predicate, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DailyInventory>> GetManyAsync(Expression<Func<DailyInventory, bool>>? predicate, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AdOneAsync(DailyInventory entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddManyAsync(IEnumerable<DailyInventory> entities, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOneAsync(DailyInventory entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(IEnumerable<DailyInventory> entities, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }
}