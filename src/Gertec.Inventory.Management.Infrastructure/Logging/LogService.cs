using Gertec.Inventory.Management.Domain.Common.Primitives;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;
using Serilog;

namespace Gertec.Inventory.Management.Infrastructure.Logging;

public class LogService(ILogger logger) : ILogService
{
    public void LogInformation(string source, string message, RequestDetails? request = default)
    {
        logger.Information("Source: {@Source}, Message: {@Message}, Request: {@RequestDetails}", source, message, request);
    }

    public void LogWarning(string source, string message, RequestDetails? request = default)
    {
        logger.Warning("Source: {@Source}, Message: {@Message}, Request: {@RequestDetails}", source, message, request);
    }

    public void LogError(string source, Exception exception, RequestDetails? request = default)
    {
        logger.Error("Source: {@Source}, Message: {@Message}, Request: {@RequestDetails}, Exception: {@Exception}", source,
            exception.InnerException?.Message ?? exception.Message, request, exception);
    }
    
    public void LogDebug(string source, string message)
    {
        logger.Debug("Source: {@Source}, Message: {@Message}", source, message);
    }
}