using MediatR;
using RentACarProject.Application.Features.Mediator.Results.SocialMediaResults;

namespace RentACarProject.Application.Features.Mediator.Queries.SocialMediaQueries
{
    public class GetSocialMediaQuery : IRequest<List<GetSocialMediaQueryResult>>
    {
    }
}
