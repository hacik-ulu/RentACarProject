using MediatR;
using RentACarProject.Application.Features.Mediator.Results.FeatureResults;

namespace RentACarProject.Application.Features.Mediator.Queries.FeatureQueries
{
    public class GetFeatureQuery : IRequest<List<GetFeatureQueryResult>>
    {
    }
}
