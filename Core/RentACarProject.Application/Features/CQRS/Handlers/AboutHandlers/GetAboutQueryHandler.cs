using RentACarProject.Application.Features.CQRS.Results.AboutResults;
using RentACarProject.Application.Interfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers;

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
