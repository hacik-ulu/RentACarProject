using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.BlogQueries;
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
    public class GetLastEightBlogsQueryHandler : IRequestHandler<GetLastEightBlogsQuery, List<GetLastEightBlogsQueryResult>>
    {
        private readonly IBlogRepository _repository;

        public GetLastEightBlogsQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLastEightBlogsQueryResult>> Handle(GetLastEightBlogsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetLastEightBlogsAsync();
            return values.Select(x => new GetLastEightBlogsQueryResult
            {
                BlogID = x.BlogID,
                Title = x.Title
            }).ToList();
        }

    }
}
