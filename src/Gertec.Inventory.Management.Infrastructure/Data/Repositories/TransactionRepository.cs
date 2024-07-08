using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Transactions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class TransactionRepository : DefaultRepository, ITransactionRepository
{
    private readonly IDbSession _dbSession;
    public TransactionRepository(IDbSession dbSession) : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<TransactionDto?> GetOneAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))
            .FirstOrDefault()?
            .Adapt<TransactionDto>();
    }

    public async Task<IEnumerable<TransactionDto>> GetManyAsync(Expression<Func<Transaction, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return (await _dbSession.Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<TransactionDto>>();
    }

    public async Task AdOneAsync(AddTransactionDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? Connection;
        var entity = model.Adapt<Transaction>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<AddTransactionDto> models, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<Transaction>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }
}