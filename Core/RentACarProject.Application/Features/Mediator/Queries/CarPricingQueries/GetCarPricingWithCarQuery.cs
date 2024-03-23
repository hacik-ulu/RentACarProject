using MediatR;
using RentACarProject.Application.Features.Mediator.Results.CarPricingResults;

namespace RentACarProject.Application.Features.Mediator.Queries.CarPricingQueries
{
    public class GetCarPricingWithCarQuery : IRequest<List<GetCarPricingWithCarQueryResult>>
    {
    }
}
