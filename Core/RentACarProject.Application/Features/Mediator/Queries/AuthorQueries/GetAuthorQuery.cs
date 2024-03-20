using MediatR;
using RentACarProject.Application.Features.Mediator.Results.AuthorResults;

namespace RentACarProject.Application.Features.Mediator.Queries.AuthorQueries
{
    public class GetAuthorQuery : IRequest<List<GetAuthorQueryResult>>
    {
    }
}
