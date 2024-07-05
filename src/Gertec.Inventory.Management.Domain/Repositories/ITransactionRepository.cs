using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Entities;

namespace Gertec.Inventory.Management.Domain.Repositories;

public interface ITransactionRepository : IReadOperations<Transaction, Guid>, ICreateOperations<Transaction>,
    IUpdateOperations<Transaction>;