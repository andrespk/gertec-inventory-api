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
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class DailyInventoryRepository : DefaultRepository, IDailyInventoryRepository
{
    private const string EntityName = nameof(DailyInventory);

    public DailyInventoryRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<DailyInventoryDto?> GetOneAsync(Expression<Func<DailyInventory, bool>> predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<DailyInventoryDto>();
    }

    public async Task<IEnumerable<DailyInventoryDto>> GetManyAsync(Expression<Func<DailyInventory, bool>>? predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<DailyInventoryDto>>();
    }

    public async Task AdOneAsync(AddDailyInventoryDto model, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entity = model.Adapt<DailyInventory>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<AddDailyInventoryDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<DailyInventory>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }

    public async Task UpdateOneAsync(UpdateDailyInventoryDto model, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var existing = await GetOneAsync(x => x.Id == model.ItemId, cancellationToken);

        if (existing is not null)
        {
            var connection = transaction?.Connection ?? Connection;
            var entity = model.Adapt<DailyInventory>();
            using (connection)
            {
                await connection.InsertAsync(entity, cancellationToken: token);
            }
        }
    }

    public async Task UpdateManyAsync(IEnumerable<UpdateDailyInventoryDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        foreach (var model in models)
            await UpdateOneAsync(model, transaction, cancellationToken);
    }

    public async Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId,
        CancellationToken? cancellationToken)
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
}