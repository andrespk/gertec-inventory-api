using Gertec.Inventory.Management.Domain.Primitives;

namespace Gertec.Inventory.Management.Domain.Models.Transactions;

public record TransactionDto(
    Guid Id,
    Guid itemId,
    DateTime InventoriedAtUtc,
    DateTime CreatedAt,
    TransactionTypes Type,
    decimal Quantity,
    decimal Amount,
    decimal UnitPrice);