using RentACarProject.Application.Features.CQRS.Queries.BannerQueries;
using RentACarProject.Application.Features.CQRS.Results.BannerResults;
using RentACarProject.Application.Interfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.ReadOperations;

public class GetBannerByIdQueryHandler
{
    private readonly IRepository<Banner> _repository;

    public GetBannerByIdQueryHandler(IRepository<Banner> repository)
    {
        _repository = repository;
    }

    public async Task<GetBannerByIdQueryResult> Handle(GetBannerByIdQuery query)
    {
        var value = await _repository.GetByIdAsync(query.Id);
        return new GetBannerByIdQueryResult
        {
            BannerID = value.BannerID,
            Description = value.Description,
            Title = value.Title,
            VideoDescription = value.VideoDescription,
            VideoUrl = value.VideoUrl
        };

    }
}
