using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using UdemyCarBook.Application.Features.CQRS.Commands.CarCommands;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.WriteOperations;

public class RemoveCarCommandHandler
{
    private readonly IRepository<Car> _repository;

    public RemoveCarCommandHandler(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveCarCommand command)
    {
        var values = await _repository.GetByIdAsync(command.Id);
        await _repository.RemoveAsync(values);
    }
}
