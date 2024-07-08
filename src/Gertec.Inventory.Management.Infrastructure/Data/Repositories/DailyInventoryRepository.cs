using System.Data;
using System.Linq.Expressions;
using DapperQueryBuilder;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Domain.ValueObjects;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class DailyInventoryRepository : DefaultRepository, IDailyInventoryRepository
{
    private readonly IDbSession _dbSession;
    public DailyInventoryRepository(IDbSession dbSession) : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<DailyInventoryDto?> GetOneAsync(Expression<Func<DailyInventory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<DailyInventoryDto>();
    }

    public async Task<IEnumerable<DailyInventoryDto>> GetManyAsync(Expression<Func<DailyInventory, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<DailyInventoryDto>>();
    }

    public async Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId,
        CancellationToken cancellationToken)
    {
        var initialBalance = new Balance(InventoryConstants.ItemDefaultMinimumQuantity,
            InventoryConstants.ItemDefaultMinimumAmount);
        var previousInventoryDate = Connection.QueryBuilder(@$"SELECT MAX(inventory_date) FROM items_daily_inventory
                                    WHERE item_id = {itemId} AND inventory_date < {inventoryDate}")
            .QueryFirstOrDefault<DateTime?>();

        if (previousInventoryDate is null)
            return initialBalance;

        var inventory = (await GetManyAsync(x => x.Id == itemId && x.Date == inventoryDate, cancellationToken))?
            .FirstOrDefault();

        return inventory?.Balance ?? initialBalance;
    }

    public async Task<DailyInventoryDto?> GetByItemIdAndDateAsync(Guid itemId, DateTime date,
        CancellationToken cancellationToken)
        => (await GetManyAsync(x => x.Id == itemId && x.Date == date, cancellationToken))?
            .FirstOrDefault();

    public async Task IncreaseAsync(IncreaseDailyInventoryDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? Connection;
        var existing = await GetByItemIdAndDateAsync(model.ItemId, model.Date, token);

        var entity = model.Adapt<DailyInventory>();
        entity.Increase(model.Adapt<Balance>());

        if (existing is not null)
            await connection.UpdateAsync(entity, x => x.Item == entity.Item && x.Date == entity.Date,
                cancellationToken: token);
        else
            await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task DecreaseAsync(DecreaseDailyInventoryDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var existing = await GetByItemIdAndDateAsync(model.ItemId, model.Date, token);

        if (existing is not null)
        {
            var connection = _dbSession.Transaction?.Connection ?? Connection;
           
            var entity = model.Adapt<DailyInventory>();
            entity.Decrease(model.Quantity);
            
            await connection.UpdateAsync(entity, x => x.Item == entity.Item && x.Date == entity.Date,
                cancellationToken: token);
        }
    }
}