using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class TransactionRepository : DefaultRepository, ITransactionRepository
{
    public TransactionRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Transaction?> GetOneAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken? cancellationToken)
        => (await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken))).FirstOrDefault();

    public async Task<IEnumerable<Transaction>> GetManyAsync(Expression<Func<Transaction, bool>>? predicate,
        CancellationToken? cancellationToken)
        => await Connection.SelectAsync(predicate,
            cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken));

    public async Task AdOneAsync(Transaction entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<Transaction> entities, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }
}