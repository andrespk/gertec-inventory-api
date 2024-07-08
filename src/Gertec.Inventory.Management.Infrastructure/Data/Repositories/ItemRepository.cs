using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Dommel;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Items;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class ItemRepository : DefaultRepository, IItemRepository
{
    private readonly IDbSession _dbSession;
    public ItemRepository(IDbSession dbSession) : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<ItemDto?> GetOneAsync(Expression<Func<Item, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<ItemDto>();
    }

    public async Task<IEnumerable<ItemDto>> GetManyAsync(Expression<Func<Item, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<ItemDto>>();
    }


    public async Task AdOneAsync(AddItemDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        var entity = model.Adapt<Item>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<AddItemDto> models, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        var entities = models.Adapt<IEnumerable<Item>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }

    public async Task UpdateOneAsync(UpdateItemDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        var entity = model.Adapt<Item>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task UpdateManyAsync(IEnumerable<UpdateItemDto> models, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        foreach (var model in models)
            await UpdateOneAsync(model, cancellationToken);
    }


    public async Task DeleteOneAsync(Guid id, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        await connection.DeleteAsync<Item>(x => x.Id == id, cancellationToken: token);
    }

    public async Task DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        await connection.DeleteMultipleAsync<Item>(x => ids.Contains(x.Id));
    }
}