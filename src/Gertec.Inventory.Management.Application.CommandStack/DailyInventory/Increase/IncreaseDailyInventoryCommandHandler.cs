using FluentValidation;
using Gertec.Inventory.Management.Domain.Models.DailyInventory;
using Gertec.Inventory.Management.Domain.Repositories;
using MediatR;

namespace Gertec.Inventory.Management.Application.CommandStack.DailyInventory.Increase;

public class IncreaseDailyInventoryCommandHandler : IRequestHandler<IncreaseDailyInventoryCommand, DailyInventoryDto>
{
    private readonly IDailyInventoryRepository _repository;
    private readonly IncreaseDailyInventoryCommandValidator _validator = new ();

    public IncreaseDailyInventoryCommandHandler(IDailyInventoryRepository repository)
    {
        _repository = repository;
    }
    public async Task<DailyInventoryDto> Handle(IncreaseDailyInventoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request);
        await _repository.
    }
}