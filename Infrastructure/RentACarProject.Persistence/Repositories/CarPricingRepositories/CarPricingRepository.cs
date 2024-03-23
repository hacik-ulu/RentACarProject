using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.CarPricingInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CarPricingRepositories
{
    public class CarPricingRepository : ICarPricingRepository
    {
        private readonly RentACarContext _context;

        public CarPricingRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<CarPricing>> GetCarPricingWithCarsAsync()
        {
            var values = await _context.CarPricings.Include(x => x.Car).ThenInclude(y => y.Brand).Include(x => x.Pricing).Where(z => z.PricingID == 3).ToListAsync();
            return values;
        }
    }
}
