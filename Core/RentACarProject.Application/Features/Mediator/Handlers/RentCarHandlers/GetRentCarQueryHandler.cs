using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.PricingQueries;
using RentACarProject.Application.Features.Mediator.Queries.RentCarQueries;
using RentACarProject.Application.Features.Mediator.Results.PricingResults;
using RentACarProject.Application.Features.Mediator.Results.RentCarResults;
using RentACarProject.Application.Interfaces.RentCarInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.RentCarHandlers
{
    public class GetRentCarQueryHandler : IRequestHandler<GetRentCarQuery, List<GetRentCarQueryResult>>
    {
        private readonly IRentCarRepository _repository;

        public GetRentCarQueryHandler(IRentCarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetRentCarQueryResult>> Handle(GetRentCarQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByFilterAsync(x => x.LocationID == request.LocationID && x.Available == true);
            var results = values.Select(y => new GetRentCarQueryResult
            {
                CarID = y.CarID,
                Brand = y.Car.Brand.Name,
                Model = y.Car.Model,
                CoverImageUrl = y.Car.CoverImagerUrl
            }).ToList();
            return results;
        }
    }
}
