using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.ReviewInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.ReviewsRepositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RentACarContext _context;
        public ReviewRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewsByCarIDAsync(int id)
        {
            var values = await _context.Reviews.Where(x => x.CarID == id).ToListAsync();
            return values;
        }
    }
}
