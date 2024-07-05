namespace Gertec.Inventory.Management.Domain.ValueObjects;

public record Balance(decimal Quantity, decimal Amount, decimal? UnitPrice = default);