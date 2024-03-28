using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.TagCloudQueries;
using RentACarProject.Application.Features.Mediator.Results.FooterAddressResults;
using RentACarProject.Application.Features.Mediator.Results.TagCloudResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.TagCloudHandlers.ReadOperations
{
    public class GetTagCloudByIdQueryHandler : IRequestHandler<GetTagCloudByIdQuery, GetTagCloudByIdQueryResult>
    {
        private readonly IRepository<TagCloud> _repository;

        public GetTagCloudByIdQueryHandler(IRepository<TagCloud> repository)
        {
            _repository = repository;
        }
        public async Task<GetTagCloudByIdQueryResult> Handle(GetTagCloudByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetTagCloudByIdQueryResult
            {
                TagCloudID = values.TagCloudID,
                Name = values.Name,
                BlogID = values.BlogID
            };
        }
    }
}
