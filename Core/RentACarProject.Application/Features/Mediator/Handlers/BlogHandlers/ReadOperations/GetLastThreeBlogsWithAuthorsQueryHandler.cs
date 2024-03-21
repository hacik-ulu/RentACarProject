using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.AuthorQueries;
using RentACarProject.Application.Features.Mediator.Results.BlogResults;
using RentACarProject.Application.Interfaces.BlogInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.BlogHandlers.ReadOperations
{
    public class GetLastThreeBlogsWithAuthorsQueryHandler : IRequestHandler<GetLastThreeBlogsWithAuthorsQuery, List<GetLastThreeBlogsWithAuthorsQueryResult>>
    {

        private readonly IBlogRepository _repository;

        public GetLastThreeBlogsWithAuthorsQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLastThreeBlogsWithAuthorsQueryResult>> Handle(GetLastThreeBlogsWithAuthorsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetLastThreeBlogsWithAuthorsAsync();
            return values.Select(x => new GetLastThreeBlogsWithAuthorsQueryResult
            {
                AuthorID = x.AuthorID,
                BlogID = x.BlogID,
                CategoryID = x.CategoryID,
                CoverImageUrl = x.CoverImageUrl,
                CreatedDate = x.CreatedDate,
                Title = x.Title,
                AuthorName = x.Author.Name
            }).ToList();
        }

    }
}
