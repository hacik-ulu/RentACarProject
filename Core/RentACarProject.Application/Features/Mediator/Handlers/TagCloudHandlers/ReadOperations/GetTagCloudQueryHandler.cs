using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.TagCloudQueries;
using RentACarProject.Application.Features.Mediator.Results.TagCloudResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.TagCloudHandlers.ReadOperations
{
    public class GetTagCloudQueryHandler : IRequestHandler<GetTagCloudQuery, List<GetTagCloudQueryResult>>
    {
        private readonly IRepository<TagCloud> _repository;

        public GetTagCloudQueryHandler(IRepository<TagCloud> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetTagCloudQueryResult>> Handle(GetTagCloudQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetTagCloudQueryResult
            {
                TagCloudID = x.TagCloudID,
                Name = x.Name,
                BlogID = x.BlogID
            }).ToList();
        }
    }
}
