using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.AuthorQueries;
using RentACarProject.Application.Features.Mediator.Results.AuthorResults;
using RentACarProject.Application.Interfaces.AuthorInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.AuthorHandlers.ReadOperations
{
    public class GetBlogsByAuthorIdQueryHandler : IRequestHandler<GetBlogsByAuthorIdQuery, List<GetBlogsByAuthorIdQueryResult>>
    {
        private readonly IAuthorRepository _repository;
        public GetBlogsByAuthorIdQueryHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetBlogsByAuthorIdQueryResult>> Handle(GetBlogsByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetBlogListByAuthorIdAsync(request.AuthorID);
            return values.Select(x => new GetBlogsByAuthorIdQueryResult
            {
                BlogID = x.BlogID,
                Title = x.Title,
                CategoryName = x.Category.Name,
            }).ToList();
        }
    }
}
