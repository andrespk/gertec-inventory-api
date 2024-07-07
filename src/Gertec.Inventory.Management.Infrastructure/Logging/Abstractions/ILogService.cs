using Microsoft.AspNetCore.Mvc;

namespace Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;

public interface ILogService
{
    Task LogInformation(string source, string Message, string? request);
    Task LogWarning(string source, string Message, string? request);
    Task LogError(string source, string Message, Exception exception);
    Task LogError(string source, string Message, ProblemDetails exception);
}