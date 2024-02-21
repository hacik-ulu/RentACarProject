﻿using RentACarProject.Application.Features.CQRS.Queries.AboutQueries;
using RentACarProject.Application.Features.CQRS.Results.AboutResults;
using RentACarProject.Application.Interfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers;

public class GetAboutByIdQueryHandler
{
    private readonly IRepository<About> _repository;

    public GetAboutByIdQueryHandler(IRepository<About> repository)
    {
        _repository = repository;
    }

    public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery query)
    {
        // values'dan sonuç olarak gelecek değerleri GetAboutByIdQueryResult tipine çevirmeliyiz.
        var values = await _repository.GetByIdAsync(query.Id);
        return new GetAboutByIdQueryResult
        {
            AboutId = values.AboutId,
            Description = values.Description,
            ImageUrl = values.ImageUrl,
            Title = values.Title,
        };
    }

}
