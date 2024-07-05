using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Database.Helpers;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class ItemRepository : DefaultRepository<Item>, IItemRepository
{
    private const string EntityName = nameof(Item);

    public string TableName { get; init; }

    public ItemRepository(DbContext dbContext) : base(dbContext)
    {
        TableName = DbHelper.GetTableName<Item>();
    }

    public async Task<Item> GetOneAsync(Expression<Func<Item, bool>>? predicate, CancellationToken? cancellationToken)
        => (await Connection.SelectAsync(predicate, cancellationToken: cancellationToken.Value)).FirstOrDefault();


    public async Task<IEnumerable<Item>> GetManyAsync(Expression<Func<Item, bool>>? predicate,
        CancellationToken? cancellationToken)
        => await Connection.SelectAsync(predicate, cancellationToken: cancellationToken.Value);

    public async Task AdOneAsync(Item entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: cancellationToken.Value);
    }

    public async Task AddManyAsync(IEnumerable<Item> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var connection = transaction?.Connection ?? Connection;
        await connection.BulkInsertAsync(entities, cancellationToken: cancellationToken.Value);
    }

    public async Task UpdateOneAsync(Item entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: cancellationToken.Value);
    }

    public async Task UpdateManyAsync(IEnumerable<Item> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var connection = transaction?.Connection ?? Connection;
        foreach(var entity in entities)
            await connection.UpdateAsync(entity, cancellationToken: cancellationToken.Value);
    }
}