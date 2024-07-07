using System.Data;
using System.Linq.Expressions;
using DapperQueryBuilder;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class DailyInventoryRepository : DefaultRepository, IDailyInventoryRepository
{
    private const string EntityName = nameof(DailyInventory);

    public DailyInventoryRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<DailyInventory?> GetOneAsync(Expression<Func<DailyInventory, bool>> predicate,
        CancellationToken? cancellationToken)
        => (await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?.FirstOrDefault();

    public async Task<IEnumerable<DailyInventory>> GetManyAsync(Expression<Func<DailyInventory, bool>>? predicate,
        CancellationToken? cancellationToken)
        => await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken));

    public async Task AdOneAsync(DailyInventory entity, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<DailyInventory> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }

    public async Task UpdateOneAsync(DailyInventory entity, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;

        using (connection)
            await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task UpdateManyAsync(IEnumerable<DailyInventory> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        foreach (var entity in entities)
            await connection.UpdateAsync(entity, cancellationToken: token);
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