using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Interfaces.CarInterfaces;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;

public class GetLastFiveCarsWithBrandsQueryHandler
{
    private readonly ICarRepository _repository;

    public GetLastFiveCarsWithBrandsQueryHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetLastFiveCarsWithBrandsQueryResult>> Handle()
    {
        var values = await _repository.GetLastFiveCarsWithBrandsAsync();
        return values.Select(x => new GetLastFiveCarsWithBrandsQueryResult
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
