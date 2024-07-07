using DeclarativeSql;
using DeclarativeSql.Annotations;
using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Primitives;
using Gertec.Inventory.Management.Domain.Validators;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Entities;

[Table(DbKind.MySql, "daily_inventory")]
public class DailyInventory : DefaultEntityBase
{
    private const int InitialQuantity = 0;
    private const int InitialAmount = 0;
    private readonly TransactionValidator _transactionValidator = new();
    private readonly DailyInventoryValidator _validator = new();

    public DailyInventory(Item item, DateTime inventoryDate, Balance initialBalance)
    {
        Item = item;
        Date = inventoryDate.Date;
        Balance = initialBalance;
    }

    public DailyInventory(Item item, DateTime inventoryDate, Balance initialBalance,
        IList<Transaction>? transactions = default)
        : this(item, inventoryDate, initialBalance)
    {
        Transactions = transactions ?? Transactions;
        UpdateBalance(Transactions, initialBalance);
    }

    public Item Item { get; }
    public DateTime Date { get; }
    public IList<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public Balance Balance { get; private set; } = new(InitialQuantity, InitialAmount);

    public void IncreaseInventory(IList<Balance> inputs)
    {
        var transactions = inputs?.Select(x =>
                new Transaction(Item, Date, DateTime.UtcNow, TransactionTypes.Incoming, x.Quantity, x.Amount,
                    x.UnitPrice))
            .ToList();
        UpdateInventory(transactions);
    }

    public void DecreaseInventory(decimal quantity)
    {
        var transactions = new List<Transaction>();
        var amount = quantity * Balance.UnitPrice;
        transactions.Add(new Transaction(Item, Date, DateTime.UtcNow, TransactionTypes.Outgoing, quantity, amount,
            Balance.UnitPrice));
        UpdateInventory(transactions);
    }

    private void UpdateInventory(List<Transaction>? transactions)
    {
        if (transactions is not null)
        {
            UpdateBalance(transactions, Balance);
            Transactions = Transactions.Concat(transactions).ToList();
        }
    }

    private void UpdateBalance(IList<Transaction>? transactions, Balance previousBalance)
    {
        if (transactions is not null && transactions.Any())
        {
            var incoming = GetInOrOutTransactionsSummary(transactions, TransactionTypes.Incoming);
            var outgoing = GetInOrOutTransactionsSummary(transactions, TransactionTypes.Outgoing);
            var totalQuantity = previousBalance.Quantity + incoming.Quantity - outgoing.Quantity;
            var totalAmount = TruncateValue(previousBalance.Amount + incoming.Amount - outgoing.Amount, 2);
            var averageUnitPrice = TruncateValue(totalQuantity > 0 ? totalAmount / totalQuantity : 0, 4);

            Balance = new Balance(totalQuantity, totalAmount, averageUnitPrice);
        }
        else
        {
            Balance = previousBalance;
        }

        _validator.ValidateAndThrow(this);
    }

    private Balance GetInOrOutTransactionsSummary(IList<Transaction> transactions, TransactionTypes type)
    {
        return transactions
            .Where(x => x.Type == type && x.Item == Item && x.InventoriedAtUtc == Date)
            .Select(x => new { x.Item, x.Quantity, x.Amount })
            .GroupBy(x => x.Item, x => x, (k, g) =>
                new Balance(g.Sum(x => x.Quantity), g.Sum(x => x.Amount)
                ))
            .FirstOrDefault();
    }

    private decimal TruncateValue(decimal value, int precision)
    {
        var factor = int.Parse($"1{new string('0', precision)}");
        return Math.Truncate(factor * value) / factor;
    }
}