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

        public void ChangeCarFeatureAvailabilityToFalse(int id)
        {
            var values = _context.CarFeatures.Where(x => x.CarFeatureID == id).FirstOrDefault();
            values.Availability = false;
            _context.SaveChanges();
        }

        public async void ChangeCarFeatureAvailabilityToTrue(int id)
        {
            var values = _context.CarFeatures.Where(x => x.CarFeatureID == id).FirstOrDefault();
            values.Availability = true;
            _context.SaveChanges();
        }

        public void CreateCarFeatureByCar(CarFeature carFeature)
        {
            _context.CarFeatures.Add(carFeature);
            _context.SaveChanges();
        }

        public async Task<List<CarFeature>> GetCarFeaturesByCarIDAsync(int carID)
        {
            var values = await _context.CarFeatures.Include(y => y.Feature).Where(x => x.CarID == carID).ToListAsync();
            return values;
        }
    }
}
