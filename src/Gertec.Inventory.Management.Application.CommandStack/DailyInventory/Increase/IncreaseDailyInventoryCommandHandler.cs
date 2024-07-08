using FluentValidation;
using Gertec.Inventory.Management.Domain.Abstractions;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.Repositories;
using Mapster;
using MediatR;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Increase;

public class IncreaseDailyInventoryCommandHandler(IDbContext dbContext, IDailyInventoryRepository repository)
    : IRequestHandler<IncreaseDailyInventoryCommand, DailyInventoryDto?>
{
    private readonly IncreaseDailyInventoryCommandValidator _validator = new ();

    public async Task<DailyInventoryDto?> Handle(IncreaseDailyInventoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var connection = dbContext.GetConnection();
        connection.Open();
        
        repository.SetConnection(connection);
        
        var transaction = connection.BeginTransaction();

        using (transaction)
        {
            await repository.IncreaseAsync(request.Adapt<IncreaseDailyInventoryDto>(), cancellationToken, transaction);
            transaction.Commit();
        }

        return await repository.GetByItemIdAndDateAsync(request.ItemId, request.Date, cancellationToken);
    }
}