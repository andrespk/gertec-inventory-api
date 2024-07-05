using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.ValueObjects;

namespace Gertec.Inventory.Management.Domain.Entities;

public class DailyInventory : DefaultEntityBase
{
    public Item Item { get; private set; }
    public DateTime Date { get; private set; }
    public IList<Transaction> Transactions { get; private set; }
    public Balance Balance { get; private set; }

    public DailyInventory(Item item, DateTime date, Balance previousBalance, IList<Transaction>? transactions = default)
    {
        Item = item;
        Date = date;
        Transactions = transactions ?? new List<Transaction>();
        Balance = new Balance(0, 0, 0);

        RefreshBalance(Transactions, previousBalance);
    }

    private void RefreshBalance(IList<Transaction> transactions, Balance previousBalance)
    {
        if (transactions.Any())
        {
            var incoming = GetInOrOutTransactionsSummary(transactions, TransactionTypes.Incoming);
            var outgoing = GetInOrOutTransactionsSummary(transactions, TransactionTypes.Outgoing);
            var totalQuantity = previousBalance.Quantity + incoming.Quantity - outgoing.Quantity;
            var totalAmount = previousBalance.Amount + incoming.Amount - outgoing.Amount;
            var averageUnitPrice = TruncateUnitPrice(totalQuantity > 0 ? totalAmount / totalQuantity : 0, 4);

            Balance = new Balance(totalQuantity, totalAmount, averageUnitPrice);
        }
        else
            Balance = previousBalance;
    }

    private Balance GetInOrOutTransactionsSummary(IList<Transaction> transactions, TransactionTypes type) =>
        transactions
            .Where(x => x.Type == type && x.Item == Item && x.CreatedAtOnUtc.Date == Date)
            .Select(x => new { x.Item, x.Quantity, x.Amount })
            .GroupBy(x => x.Item, x => x, (k, g) =>
                new Balance(g.Sum(x => x.Quantity), g.Sum(x => x.Amount)
                ))
            .FirstOrDefault();

    private decimal TruncateUnitPrice(decimal value, int precision)
    {
        var factor = int.Parse($"1{new String('0', precision)}");
        return Math.Truncate(factor * value) / factor;
    }
}