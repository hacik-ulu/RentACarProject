using RentACarProject.Application.Features.CQRS.Results.BrandResults;
using RentACarProject.Application.Interfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.ReadOperations;

public class GetBrandQueryHandler
{
    private readonly IRepository<Brand> _repository;

    public GetBrandQueryHandler(IRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetBrandQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetBrandQueryResult
        {
            BrandID = x.BrandID,
            Name = x.Name
        }).ToList();
    }
}
