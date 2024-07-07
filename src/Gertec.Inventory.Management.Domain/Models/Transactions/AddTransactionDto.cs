using Gertec.Inventory.Management.Domain.Primitives;

namespace Gertec.Inventory.Management.Domain.Models.Transactions;

public record AddTransactionDto(
    Guid itemId,
    DateTime InventoriedAtUtc,
    TransactionTypes Type,
    decimal Quantity,
    decimal Amount,
    decimal UnitPrice);