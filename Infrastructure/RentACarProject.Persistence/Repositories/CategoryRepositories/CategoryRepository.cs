using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using RentACarProject.Application.Interfaces.CategoryInterfaces;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RentACarContext _context;

        public CategoryRepository(RentACarContext context)
        {
            _context = context;
        }
        public async Task<GetCategoryWithBlogCountResult> GetCategoryBlogCountAsync(int categoryId)
        {
            var result = await _context.Categories
                .Where(c => c.CategoryID == categoryId)
            .GroupJoin(
                    _context.Blogs,
                    category => category.CategoryID,
                    blog => blog.CategoryID,
                    (category, blogs) => new GetCategoryWithBlogCountResult
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.Name,
                        BlogCount = category.Blogs.Count()
                    })
                .FirstOrDefaultAsync();
            return result;
        }

    }
}
