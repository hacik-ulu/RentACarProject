using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;

namespace RentACarProject.Persistence.Repositories.CarRepository;

public class CarRepository : ICarRepository
{
    private readonly RentACarContext _context;

    public CarRepository(RentACarContext context)
    {
        _context = context;
    }

    public async Task<int> GetCarCountAsync()
    {
        var value = await _context.Cars.CountAsync();
        return value;
    }

    public async Task<List<Car>> GetCarsListWithBrandsAsync()
    {
        var values = await _context.Cars.Include(x => x.Brand).ToListAsync();
        return values;
    }

    public async Task<List<Car>> GetLastFiveCarsWithBrandsAsync()
    {
        var values = await _context.Cars.Include(x => x.Brand).OrderByDescending(y => y.CarID).Take(5).ToListAsync();
        return values;
    }
}
