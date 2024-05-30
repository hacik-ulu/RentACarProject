using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.CarDescriptionInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CarDescriptionRepositories
{
    public class CarDescriptionRepository : ICarDescriptionRepository
    {
        private readonly RentACarContext _context;

        public CarDescriptionRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<CarDescription> GetCarDescriptionAsync(int carID)
        {
            var values = await _context.CarDescriptions.Where(x => x.CarID == carID).FirstOrDefaultAsync();
            return values;
        }
    }
}
