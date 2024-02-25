using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;

public class UpdateCategoryCommandHandler
{
    private readonly IRepository<Category> _repository;

    public UpdateCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateCategoryCommand command)
    {
        var value = await _repository.GetByIdAsync(command.CategoryID);
        value.Name = command.Name;
        await _repository.UpdateAsync(value);
    }
}
