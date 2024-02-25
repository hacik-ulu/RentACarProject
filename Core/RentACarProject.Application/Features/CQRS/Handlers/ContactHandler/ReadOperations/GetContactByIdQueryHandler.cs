using RentACarProject.Application.Features.CQRS.Queries.ContactQueries;
using RentACarProject.Application.Features.CQRS.Results.AboutResults;
using RentACarProject.Application.Features.CQRS.Results.ContactResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.ContactHandler.ReadOperations;

public class GetContactByIdQueryHandler
{
    private readonly IRepository<Contact> _repository;

    public GetContactByIdQueryHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task<GetContactByIdQueryResult> Handle(GetContactByIdQuery query)
    {
        var values = await _repository.GetByIdAsync(query.Id);
        return new GetContactByIdQueryResult
        {
            ContactID = values.ContactID,
            Name = values.Name,
            Email = values.Email,
            Subject = values.Subject,
            Message = values.Message,
            SendDate = values.SendDate,
        };
    }
}
