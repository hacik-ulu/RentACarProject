using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.WriteOperations;

public class CreateCategoryCommandHandler
{
    private readonly IRepository<Category> _repository;

    public CreateCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateCategoryCommand command)
    {
        await _repository.CreateAsync(new Category
        {
            Name = command.Name
        });
    }
}
