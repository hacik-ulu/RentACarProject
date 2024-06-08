using RentACarProject.Application.Features.CQRS.Queries.BrandQueries;
using RentACarProject.Application.Features.CQRS.Queries.CarQueries;
using RentACarProject.Application.Features.CQRS.Results.CarResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.ReadOperations;

public class GetCarByIdQueryHandler
{
    private readonly IRepository<Car> _carRepository;
    private readonly IRepository<Brand> _brandRepository;

    public GetCarByIdQueryHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
    }

    public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
    {

        var cars = await _carRepository.GetAllAsync();
        var brands = await _brandRepository.GetAllAsync();

        var queryResult = (from car in cars
                           join brand in brands
                           on car.BrandID equals brand.BrandID
                           where car.CarID == query.Id
                           select new GetCarByIdQueryResult
                           {
                               CarID = car.CarID,
                               BrandID = car.BrandID,
                               BrandName = brand.Name,
                               Model = car.Model,
                               CoverImagerUrl = car.CoverImagerUrl,
                               Mileage = car.Mileage,
                               Transmission = car.Transmission,
                               Seat = car.Seat,
                               Luggage = car.Luggage,
                               Fuel = car.Fuel,
                               BigImageUrl = car.BigImageUrl,
                               Year = car.Year
                           }).FirstOrDefault();


        return queryResult;
    }
}
