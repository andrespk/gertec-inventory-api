using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.Models.Transactions;
using Gertec.Inventory.Management.Domain.Repositories;
using Gertec.Inventory.Management.Infrastructure.Data.Abstractions;
using Mapster;
using MediatR;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Increase;

public class IncreaseDailyInventoryCommandHandler(
    IUnitOfWork unitOfWork,
    IDailyInventoryRepository repository,
    ITransactionRepository transactionRepository)
    : IRequestHandler<IncreaseDailyInventoryCommand, DailyInventoryDto?>
{
    private readonly IncreaseDailyInventoryCommandValidator _validator = new();

    public async Task<DailyInventoryDto?> Handle(IncreaseDailyInventoryCommand request,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        unitOfWork.BeginTransaction();
        await repository.IncreaseAsync(request.Adapt<IncreaseDailyInventoryDto>(), cancellationToken);
        unitOfWork.Commit();

        return await repository.GetByItemIdAndDateAsync(request.ItemId, request.Date, cancellationToken);
    }
}