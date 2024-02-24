using RentACarProject.Application.Features.CQRS.Queries.BrandQueries;
using RentACarProject.Application.Features.CQRS.Queries.CarQueries;
using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;

public class GetCarByIdQueryHandler
{
    private readonly IRepository<Car> _repository;

    public GetCarByIdQueryHandler(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
    {
        var values = await _repository.GetByIdAsync(query.Id);
        return new GetCarByIdQueryResult
        {
            CarID = values.CarID,
            BrandID = values.BrandID,
            Model = values.Model,
            CoverImagerUrl = values.CoverImagerUrl,
            Mileage = values.Mileage,
            Transmission = values.Transmission,
            Seat = values.Seat,
            Luggage = values.Luggage,
            Fuel = values.Fuel,
            BigImageUrl = values.BigImageUrl
        };
    }
}
