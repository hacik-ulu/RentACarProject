using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System.Reflection;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;

public class GetCarQueryHandler
{
    private IRepository<Car> _repository;

    public GetCarQueryHandler(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetCarQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetCarQueryResult
        {
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
            Year = x.Year
        }).ToList();
    }
}
