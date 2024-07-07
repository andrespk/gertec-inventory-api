using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Dommel;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class ItemRepository : DefaultRepository, IItemRepository
{
    public ItemRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Item?> GetOneAsync(Expression<Func<Item, bool>> predicate, CancellationToken? cancellationToken)
        => (await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?.FirstOrDefault();


    public async Task<IEnumerable<Item>> GetManyAsync(Expression<Func<Item, bool>>? predicate,
        CancellationToken? cancellationToken)
        => await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken));


    public async Task AdOneAsync(Item entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<Item> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }

    public async Task UpdateOneAsync(Item entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task UpdateManyAsync(IEnumerable<Item> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token =ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        foreach (var entity in entities)
            await connection.UpdateAsync(entity, cancellationToken: token);
    }


    public async Task DeleteOneAsync(Guid id, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.DeleteAsync<Item>(x => x.Id == id, cancellationToken: token);
    }

    public async Task DeleteManyAsync(IEnumerable<Guid> ids, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.DeleteMultipleAsync<Item>(x => ids.Contains(x.Id), transaction);
    }
}