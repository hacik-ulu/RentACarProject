﻿using MediatR;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;

namespace RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries
{
    public class GetCarBrandAndModelByRentPriceDailyMaxQuery : IRequest<GetCarBrandNameAndModelByRentPriceDailyMaxQueryResult>
    {
    }
}
