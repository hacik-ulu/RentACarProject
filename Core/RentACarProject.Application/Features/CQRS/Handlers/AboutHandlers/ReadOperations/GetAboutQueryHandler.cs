using RentACarProject.Application.Features.CQRS.Results.AboutResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.ReadOperations;

public class GetAboutQueryHandler
{
    private readonly IRepository<About> _repository;

    public GetAboutQueryHandler(IRepository<About> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAboutQueryResult>> Handle()
    {
        //Seçilen nesnelerin(About) tüm özellikleri tek tek GetAboutQueryResult tipine dönüştürülerek,
        //yeni bir listeden seçim yapıyoruz.

        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetAboutQueryResult
        {
            AboutId = x.AboutId,
            Description = x.Description,
            Title = x.Title,
            ImageUrl = x.ImageUrl
        }).ToList();
    }
}

// GetAaboutTry.