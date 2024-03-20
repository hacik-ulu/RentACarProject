using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.LocationQueries;
using RentACarProject.Application.Features.Mediator.Results.FooterAddressResults;
using RentACarProject.Application.Features.Mediator.Results.LocationResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.LocationHandlers.ReadOperations
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        private readonly IRepository<Location> _repository;

        public GetBlogByIdQueryHandler(IRepository<Location> repository)
        {
            _repository = repository;
        }
        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetLocationByIdQueryResult
            {
                LocationID = values.LocationID,
                Name = values.Name
            };
        }
    }
}
