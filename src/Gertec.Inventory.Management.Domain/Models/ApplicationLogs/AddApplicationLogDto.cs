namespace Gertec.Inventory.Management.Domain.Models.ApplicationLogs;

public record AddApplicationLogDto(
    string Source,
    DateTime Timestamp,
    string Message,
    string Level,
    string? Exception = default,
    string? ProblemDetails = default,
    string? Request = default);