﻿using RentACarProject.Application.Features.CQRS.Results.BannerResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.ReadOperations;

public class GetBannerQueryHandler
{
    private readonly IRepository<Banner> _repository;

    public GetBannerQueryHandler(IRepository<Banner> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetBannerQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetBannerQueryResult
        {
            BannerID = x.BannerID,
            Description = x.Description,
            Title = x.Title,
            VideoDescription = x.VideoDescription,
            VideoUrl = x.VideoUrl,
        }).ToList();
    }
}
