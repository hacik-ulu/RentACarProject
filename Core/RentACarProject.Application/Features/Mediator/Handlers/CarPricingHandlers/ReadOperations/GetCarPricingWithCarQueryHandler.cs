using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarPricingQueries;
using RentACarProject.Application.Features.Mediator.Results.CarPricingResults;
using RentACarProject.Application.Interfaces.CarPricingInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.ReadOperations
{
    public class GetCarPricingWithCarQueryHandler : IRequestHandler<GetCarPricingWithCarQuery, List<GetCarPricingWithCarQueryResult>>
    {
        private readonly ICarPricingRepository _repository;
        public GetCarPricingWithCarQueryHandler(ICarPricingRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCarPricingWithCarQueryResult>> Handle(GetCarPricingWithCarQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetCarPricingWithCarsAsync();
            return values.Select(x => new GetCarPricingWithCarQueryResult
            {
                Amount = x.Amount,
                CarPricingId = x.CarPricingID,
                CarID = x.CarID,
                Brand = x.Car.Brand.Name,
                CoverImageUrl = x.Car.CoverImagerUrl,
                Model = x.Car.Model,
            }).ToList();
        }

    }
}
