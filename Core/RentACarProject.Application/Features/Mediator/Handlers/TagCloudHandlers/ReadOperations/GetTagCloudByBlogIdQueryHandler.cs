using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.TagCloudQueries;
using RentACarProject.Application.Features.Mediator.Results.TagCloudResults;
using RentACarProject.Application.Interfaces.TagCloudInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.TagCloudHandlers.ReadOperations
{
    public class GetTagCloudByBlogIdQueryHandler : IRequestHandler<GetTagCloudByBlogIdQuery, List<GetTagCloudByBlogIdQueryResult>>
    {
        private readonly ITagCloudRepository _repository;
        public GetTagCloudByBlogIdQueryHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetTagCloudByBlogIdQueryResult>> Handle(GetTagCloudByBlogIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetTagCloudsByBlogID(request.Id); 

            return values.Select(x => new GetTagCloudByBlogIdQueryResult
            {
                Name = x.Name,
                TagCloudID = x.TagCloudID,
                BlogID = x.BlogID
            }).ToList();
        }

    }
}