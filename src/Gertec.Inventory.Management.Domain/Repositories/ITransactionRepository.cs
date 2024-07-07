using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;
using Gertec.Inventory.Management.Domain.Models.Transactions;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface ITransactionRepository : IReadOperations<Transaction, Guid, TransactionDto>,
    ICreateOperations<AddTransactionDto>;