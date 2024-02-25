using RentACarProject.Application.Features.CQRS.Commands.ContactCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.WriteOperations;

public class UpdateContactCommandHandler
{
    private readonly IRepository<Contact> _repository;

    public UpdateContactCommandHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateContactCommand command)
    {
        var values = await _repository.GetByIdAsync(command.ContactID);
        values.Email = command.Email;
        values.SendDate = command.SendDate;
        values.Subject = command.Subject;
        values.Message = command.Message;
        values.Name = command.Name;
        await _repository.UpdateAsync(values);
    }
}
