using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.ApplicationLogs;
using Gertec.Inventory.Management.Domain.Repositories;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class ApplicationLogRepository : DefaultRepository, IApplicationLogRepository
{
    public ApplicationLogRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ApplicationLogDto?> GetOneAsync(Expression<Func<ApplicationLog, bool>> predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<ApplicationLogDto>();
    }

    public async Task<IEnumerable<ApplicationLogDto>> GetManyAsync(Expression<Func<ApplicationLog, bool>>? predicate,
        CancellationToken? cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<ApplicationLogDto>>();
    }

    public async Task AdOneAsync(ApplicationLogDto model, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entity = model.Adapt<ApplicationLog>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<ApplicationLogDto> models, IDbTransaction? transaction,
        CancellationToken? cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<ApplicationLog>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }
}