using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.RentCarInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.RentCarRepositories
{
    public class RentCarRepository : IRentCarRepository
    {
        private readonly RentACarContext _context;
        public RentCarRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<RentCar>> GetByFilterAsync(Expression<Func<RentCar, bool>> filter)
        {
            var values = await _context.RentCars.Where(filter).ToListAsync();
            return values;
        }


    }
}
