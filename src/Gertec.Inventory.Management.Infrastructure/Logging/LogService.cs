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

        Log.Logger = new LoggerConfiguration()
            .WriteTo.MySQL(
                _configuration.GetConnectionString(DbHelper.DefaultConnectionConfigName),
                LogTableName,
                storeTimestampInUtc: true)
            .CreateLogger();
    }

    public Task LogInformation(string source, string Message, string? request)
    {
        throw new NotImplementedException();
    }

    public Task LogWarning(string source, string Message, string? request)
    {
        throw new NotImplementedException();
    }

    public Task LogError(string source, string Message, Exception exception)
    {
        throw new NotImplementedException();
    }

    public Task LogError(string source, string Message, ProblemDetails exception)
    {
        throw new NotImplementedException();
    }
}