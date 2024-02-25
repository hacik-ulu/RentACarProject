using RentACarProject.Application.Features.CQRS.Commands.AboutCommands;
using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;

public class RemoveCategoryCommandHandler
{
    private readonly IRepository<Category> _repository;

    public RemoveCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveCategoryCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        await _repository.RemoveAsync(value);
    }
}