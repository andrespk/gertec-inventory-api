using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.ApplicationLogs;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface IApplicationLogRepository : IReadOperations<ApplicationLog, int, ApplicationLogDto>,
    ICreateOperations<ApplicationLogDto>;