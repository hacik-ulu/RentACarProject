using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.BlogInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.BlogRepositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly RentACarContext _context;

        public BlogRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAllBlogsWithAuthorsAsync()
        {
            var values = await _context.Blogs.Include(x => x.Author).ToListAsync();
            return values;
        }

        public async Task<List<Blog>> GetLastThreeBlogsWithAuthorsAsync()
        {
            var values = await _context.Blogs.Include(x => x.Author).OrderByDescending(y => y.BlogID).Take(3).ToListAsync();
            return values;
        }
    }
}
