using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Transactions;
using Gertec.Inventory.Management.Domain.Repositories;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class TransactionRepository : DefaultRepository, ITransactionRepository
{
    public TransactionRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TransactionDto?> GetOneAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))
            .FirstOrDefault()?
            .Adapt<TransactionDto>();
    }

    public async Task<IEnumerable<TransactionDto>> GetManyAsync(Expression<Func<Transaction, bool>>? predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<TransactionDto>>();
    }

    public async Task AdOneAsync(AddTransactionDto model, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entity = model.Adapt<Transaction>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<AddTransactionDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<Transaction>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }
}