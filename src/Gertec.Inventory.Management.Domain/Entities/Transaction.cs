using DeclarativeSql;
using DeclarativeSql.Annotations;
using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Primitives;
using Gertec.Inventory.Management.Domain.Validators;

namespace Gertec.Inventory.Management.Domain.Entities;

[Table(DbKind.MySql, "items_transactions")]
public class Transaction : DefaultEntityBase
{
    private readonly TransactionValidator _validator = new();

    public Transaction(Item item, DateTime inventoriedAtUtc, DateTime createdAtUtc, TransactionTypes type,
        decimal quantity, decimal amount,
        decimal unitPrice)
    {
        Item = item;
        InventoriedAtUtc = inventoriedAtUtc;
        CreatedAtUtc = createdAtUtc;
        Type = type;
        Quantity = quantity;
        Amount = amount;
        UnitPrice = unitPrice;

        _validator.ValidateAndThrow(this);
    }

    public Item Item { get; private set; }
    public DateTime InventoriedAtUtc { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public TransactionTypes Type { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Amount { get; private set; }
}