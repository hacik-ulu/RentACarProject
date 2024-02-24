using RentACarProject.Application.Features.CQRS.Queries.BannerQueries;
using RentACarProject.Application.Features.CQRS.Queries.BrandQueries;
using RentACarProject.Application.Features.CQRS.Results.BannerResults;
using RentACarProject.Application.Features.CQRS.Results.BrandResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.ReadOperations;

public class GetBrandByIdQueryHandler
{
    private readonly IRepository<Brand> _repository;

    public GetBrandByIdQueryHandler(IRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task<GetBrandByIdQueryResult> Handle(GetBrandByIdQuery query)
    {
        var value = await _repository.GetByIdAsync(query.Id);
        return new GetBrandByIdQueryResult
        {
            BrandID = value.BrandID,
            Name = value.Name
        };

    }
}
