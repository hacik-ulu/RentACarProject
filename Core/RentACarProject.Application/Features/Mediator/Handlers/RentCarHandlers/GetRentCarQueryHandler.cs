using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.RentCarQueries;
using RentACarProject.Application.Features.Mediator.Results.RentCarResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.RentCarInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.RentCarHandlers
{
    public class GetRentCarQueryHandler : IRequestHandler<GetRentCarQuery, List<GetRentCarQueryResult>>
    {
        private readonly IRentCarRepository _rentCarRepository;
        private readonly IRepository<CarPricing> _carPricingRepository;

        public GetRentCarQueryHandler(IRentCarRepository rentCarRepository, IRepository<CarPricing> carPricingRepository)
        {
            _rentCarRepository = rentCarRepository;
            _carPricingRepository = carPricingRepository;
        }

        public async Task<List<GetRentCarQueryResult>> Handle(GetRentCarQuery request, CancellationToken cancellationToken)
        {

            var rentCars = await _rentCarRepository.GetByFilterAsync(x => x.LocationID == request.LocationID && x.Available == true);

            var carPricings = await _carPricingRepository.GetAllAsync();

            var results = (from rentCar in rentCars
                           join carPricing in carPricings
                           on rentCar.CarID equals carPricing.CarID
                           where carPricing.PricingID == 1
                           select new GetRentCarQueryResult
                           {
                               CarID = rentCar.CarID,
                               Brand = rentCar.Car.Brand.Name,
                               Model = rentCar.Car.Model,
                               CoverImageUrl = rentCar.Car.CoverImagerUrl,
                               Amount = carPricing.Amount
                           }).ToList();
            return results;
        }
    }
}
