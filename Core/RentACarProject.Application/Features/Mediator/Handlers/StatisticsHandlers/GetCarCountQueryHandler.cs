using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.FooterAddressResults;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarCountQueryHandler : IRequestHandler<GetCarCountQuery, GetCarCountQueryResult>
    {
        private readonly ICarRepository _repository;

        public GetCarCountQueryHandler(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarCountQueryResult> Handle(GetCarCountQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetCarCountAsync();
            return new GetCarCountQueryResult
            {
                CarCount = values
            };
        }
    }
}
