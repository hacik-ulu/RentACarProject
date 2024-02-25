using RentACarProject.Application.Features.CQRS.Commands.ContactCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.WriteOperations;

public class RemoveContactCommandHandler
{
    private readonly IRepository<Contact> _repository;

    public RemoveContactCommandHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveContactCommand command)
    {
        var values = await _repository.GetByIdAsync(command.Id);
        await _repository.RemoveAsync(values);
    }
}
