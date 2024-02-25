using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations;

public class GetCategoryQueryHandler
{
    private readonly IRepository<Category> _repository;

    public GetCategoryQueryHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetCategoryQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetCategoryQueryResult
        {
            Name = x.Name,
            CategoryID = x.CategoryID
        }).ToList();
    }
}
