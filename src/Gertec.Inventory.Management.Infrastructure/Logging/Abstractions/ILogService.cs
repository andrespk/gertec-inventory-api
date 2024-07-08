using Gertec.Inventory.Management.Domain.Common.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;

public interface ILogService
{
    void LogInformation(string source, string message, RequestDetails? request = default);
    void LogWarning(string source, string message, RequestDetails? request = default);
    void LogError(string source, Exception exception, RequestDetails? request = default);
}