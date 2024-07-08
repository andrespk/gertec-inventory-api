using System.Data;
using System.Linq.Expressions;
using DeclarativeSql;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.ApplicationLogs;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;

namespace Gertec.Inventory.Management.Infrastructure.Data.Repositories;

public class ApplicationLogRepository : DefaultRepository, IApplicationLogRepository
{
    private readonly IDbSession _dbSession;
    
    public ApplicationLogRepository(IDbSession dbSession) : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<ApplicationLogDto?> GetOneAsync(Expression<Func<ApplicationLog, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .FirstOrDefault()?
            .Adapt<ApplicationLogDto>();
    }

    public async Task<IEnumerable<ApplicationLogDto>> GetManyAsync(Expression<Func<ApplicationLog, bool>>? predicate,
        CancellationToken cancellationToken)
    {
        return (await Connection.SelectAsync(predicate,
                cancellationToken: ResolveAndConfigureCancellationToken(cancellationToken)))?
            .Adapt<IEnumerable<ApplicationLogDto>>();
    }

    public async Task AdOneAsync(ApplicationLogDto model, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? Connection;
        var entity = model.Adapt<ApplicationLog>();
        await connection.InsertAsync(entity, cancellationToken: token);
    }

    public async Task AddManyAsync(IEnumerable<ApplicationLogDto> models, CancellationToken cancellationToken)
    {
        var token = ResolveAndConfigureCancellationToken(cancellationToken);
        var connection = _dbSession.Transaction?.Connection ?? Connection;
        var entities = models.Adapt<IEnumerable<ApplicationLog>>();
        await connection.BulkInsertAsync(entities, cancellationToken: token);
    }
}