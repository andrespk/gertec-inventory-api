using System.Linq.Expressions;
using DapperQueryBuilder;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Constants;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.Models.Transactions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Domain.ValueObjects;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class DailyInventoryRepository : DefaultRepository, IDailyInventoryRepository
{ 
    private readonly IDbSession _dbSession;
    private readonly ITransactionRepository _transactionRepository;
    public DailyInventoryRepository(IDbSession dbSession, ITransactionRepository transactionRepository) : base(dbSession)
    {
        _dbSession = dbSession;
        _transactionRepository = transactionRepository;
    }

    public async Task<DailyInventoryDto?> GetOneAsync(Expression<Func<DailyInventory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<DailyInventoryDto>();
    }

    public async Task<IEnumerable<DailyInventoryDto>> GetManyAsync(Expression<Func<DailyInventory, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<DailyInventoryDto>>();
    }

    public async Task<Balance> GetInitialBalanceAsync(DateTime inventoryDate, Guid itemId,
        CancellationToken cancellationToken)
    {
        var initialBalance = new Balance(InventoryConstants.ItemDefaultMinimumQuantity,
            InventoryConstants.ItemDefaultMinimumAmount);
        var previousInventoryDate = _dbSession.Connection.QueryBuilder(@$"SELECT MAX(inventory_date) FROM items_daily_inventory
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
        var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
        var existing = await GetByItemIdAndDateAsync(model.ItemId, model.Date, token);

        var entity = model.Adapt<DailyInventory>();
        entity.Increase(model.Adapt<Balance>());

        if (existing is not null)
            await connection.UpdateAsync(entity, x => x.Item == entity.Item && x.Date == entity.Date,
                cancellationToken: token);
        else
            await connection.InsertAsync(entity, cancellationToken: token);

        await AddTransactionsAsync(entity, token);
    }

    public async Task DecreaseAsync(DecreaseDailyInventoryDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var existing = await GetByItemIdAndDateAsync(model.ItemId, model.Date, token);

        if (existing is not null)
        {
            var connection = _dbSession.Transaction?.Connection ?? _dbSession.Connection;
           
            var entity = model.Adapt<DailyInventory>();
            entity.Decrease(model.Quantity);
            
            await connection.UpdateAsync(entity, x => x.Item == entity.Item && x.Date == entity.Date,
                cancellationToken: token);

            await AddTransactionsAsync(entity, token);
        }
    }
    
    private async Task AddTransactionsAsync(DailyInventory entity, CancellationToken token)
    {
        var transactionsToAdd = entity.Transactions.Adapt<IEnumerable<AddTransactionDto>>();
        await _transactionRepository.AddManyAsync(transactionsToAdd, token);
    }
}