using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Entities;

public class Transaction : DefaultEntityBase
{
    public Item Item { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public TransactionTypes Type { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Amount { get; private set; }

    public Transaction(Item item, DateTime createdAt, TransactionTypes type, decimal quantity, decimal amount,
        decimal unitPrice)
    {
        Item = item;
        CreatedAt = createdAt;
        Type = type;
        Quantity = quantity;
        Amount = amount;
        UnitPrice = unitPrice;
    }
}