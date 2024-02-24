using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;

public class GetCarWithBrandQueryHandler
{
    private readonly ICarRepository _repository;

    public GetCarWithBrandQueryHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetCarWithBrandQueryResult>> Handle()
    {
        var values = await _repository.GetCarsListWithBrandsAsync();
        return values.Select(x => new GetCarWithBrandQueryResult
        {
            BrandName = x.Brand.Name,
            CarID = x.CarID,
            BrandID = x.BrandID,
            Model = x.Model,
            CoverImagerUrl = x.CoverImagerUrl,
            Mileage = x.Mileage,
            Transmission = x.Transmission,
            Seat = x.Seat,
            Luggage = x.Luggage,
            Fuel = x.Fuel,
            BigImageUrl = x.BigImageUrl,

        }).ToList();
    }
}
