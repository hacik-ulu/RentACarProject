using RentACarProject.Application.Features.CQRS.Queries.CarQueries;
using RentACarProject.Application.Features.CQRS.Queries.CategoryQueries;
using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations;

public class GetCategoryByIdQueryHandler
{
    private readonly IRepository<Category> _repository;

    public GetCategoryByIdQueryHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery query)
    {
        var values = await _repository.GetByIdAsync(query.Id);
        return new GetCategoryByIdQueryResult
        {
            CategoryID = values.CategoryID,
            Name = values.Name
        };
    }
}
