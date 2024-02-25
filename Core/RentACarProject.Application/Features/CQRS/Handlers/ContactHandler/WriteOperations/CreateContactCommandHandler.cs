using RentACarProject.Application.Features.CQRS.Commands.ContactCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.WriteOperations;

public class CreateContactCommandHandler
{
    private readonly IRepository<Contact> _repository;

    public CreateContactCommandHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateContactCommand command)
    {
        await _repository.CreateAsync(new Contact
        {
            Name = command.Name,
            Email = command.Email,
            Subject = command.Subject,
            Message = command.Message,
            SendDate = command.SendDate
        });
    }
}

