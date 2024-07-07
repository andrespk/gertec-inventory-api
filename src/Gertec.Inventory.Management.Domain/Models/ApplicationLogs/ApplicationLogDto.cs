namespace Gertec.Inventory.Management.Domain.Models.ApplicationLogs;

public record ApplicationLogDto(
    Guid Id,
    string Source,
    DateTime Timestamp,
    string Message,
    string Level,
    string? Exception = default,
    string? ProblemDetails = default,
    string? Request = default);