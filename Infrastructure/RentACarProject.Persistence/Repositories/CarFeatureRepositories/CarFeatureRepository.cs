using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CarFeatureRepositories
{
    public class CarFeatureRepository : ICarFeatureRepository
    {
        private readonly RentACarContext _context;

        public CarFeatureRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<CarFeature>> GetCarFeaturesByCarIDAsync(int carID)
        {
            var values = await _context.CarFeatures.Where(x => x.CarID == carID).ToListAsync();
            return values;
        }
    }
}
