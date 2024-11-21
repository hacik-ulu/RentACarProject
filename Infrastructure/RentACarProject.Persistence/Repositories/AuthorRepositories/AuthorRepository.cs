using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.AuthorInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.AuthorRepositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RentACarContext _context;

        public AuthorRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlogListByAuthorIdAsync(int id)
        {
            var values = await _context.Blogs
                .Where(b => b.AuthorID == id)
                .Include(b => b.Category)
                .ToListAsync();
            return values;
        }


    }
}
