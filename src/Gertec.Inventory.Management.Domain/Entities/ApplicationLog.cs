using System.Text.Json;
using DeclarativeSql;
using DeclarativeSql.Annotations;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Common.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Gertec.Inventory.Management.Domain.Entities;

[Table(DbKind.MySql, "application_logs")]
public class ApplicationLog : DefaultEntityBase
{
    public ApplicationLog(string source, string message, string level)
    {
        Source = source;
        Message = message;
        Level = level;
    }

    public ApplicationLog(string source, string message, string level, RequestDetails requestDetails,
        Exception exception) : this(source, message, level)
    {
        Request = JsonSerializer.Serialize(requestDetails);
        Exception = JsonSerializer.Serialize(exception);
    }

    public ApplicationLog(string source, string message, string level, RequestDetails requestDetails,
        ProblemDetails problemDetails) : this(source, message, level)
    {
        ProblemDetails = JsonSerializer.Serialize(problemDetails);
    }

    public string Message { get; init; }
    public string Level { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public string Source { get; init; }
    public string? Exception { get; init; }
    public string? ProblemDetails { get; init; }
    public string? Request { get; init; }
}