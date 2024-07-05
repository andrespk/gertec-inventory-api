using System.Data;
using System.Linq.Expressions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Repositories;

namespace Gertec.Inventory.Management.Infrastructure.Database.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task<Transaction> GetOneAsync(Expression<Func<Transaction, bool>>? predicate, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetManyAsync(Expression<Func<Transaction, bool>>? predicate, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AdOneAsync(Transaction entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddManyAsync(IEnumerable<Transaction> entities, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOneAsync(Transaction entity, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(IEnumerable<Transaction> entities, IDbTransaction? transaction, CancellationToken? cancellationToken)
    {
        throw new NotImplementedException();
    }
}