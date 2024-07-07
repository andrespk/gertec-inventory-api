using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Dommel;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Items;
using Gertec.Inventory.Management.Domain.Repositories;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class ItemRepository : DefaultRepository, IItemRepository
{
    public ItemRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ItemDto?> GetOneAsync(Expression<Func<Item, bool>> predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<ItemDto>();
    }


    public async Task<IEnumerable<ItemDto>> GetManyAsync(Expression<Func<Item, bool>>? predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<ItemDto>>();
    }


    public async Task AdOneAsync(AddItemDto model, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entity = model.Adapt<Item>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<AddItemDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<Item>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }

    public async Task UpdateOneAsync(UpdateItemDto model, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entity = model.Adapt<Item>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task UpdateManyAsync(IEnumerable<UpdateItemDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        foreach (var model in models)
            await UpdateOneAsync(model, transaction, cancellationToken);
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