using Gertec.Inventory.Management.Domain.Common.Primitives;
using Gertec.Inventory.Management.Infrastructure.Data.Helpers;
using Gertec.Inventory.Management.Infrastructure.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Gertec.Inventory.Management.Infrastructure.Logging;

public class LogService : ILogService
{
    private const string LogTableName = "application_logs";
    private readonly IConfiguration _configuration;

    public LogService(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration.GetConnectionString(DataHelper.DefaultConnectionConfigName);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.MySQL(connectionString, LogTableName, storeTimestampInUtc: true)
            .CreateLogger();
    }

    public void LogInformation(string source, string message, RequestDetails? request = default)
    {
        Log.Logger.Information("Source: {@Source}, Message: {@Message}, Request: {@Request}", source, message, request);
    }

    public void LogWarning(string source, string message, RequestDetails? request = default)
    {
        Log.Logger.Warning("Source: {@Source}, Message: {@Message}, Request: {@Request}", source, message, request);
    }

    public void LogError(string source, string message, Exception exception, RequestDetails? request = default)
    {
        Log.Logger.Error("Source: {@Source}, Message: {@Message}, Request: {@Request}, Exception: {@Exception}", source,
            message, request, exception);
    }

    public void LogError(string source, string message, ProblemDetails problemDetails, RequestDetails request)
    {
        Log.Logger.Error(
            "Source: {@Source}, Message: {@Message}, Request: {@Request}, ProblemDetails: {@ProblemDetails}", source,
            message, request, problemDetails);
    }
}