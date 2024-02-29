using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Interfaces.CarInterfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarsListWithBrandsAsync();
}
