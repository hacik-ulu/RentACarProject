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

        public async Task<List<GetCategoryWithBlogCountResult>> GetCategoriesBlogCountAsync()
        {
            var result = await _context.Categories
                .GroupJoin(
                    _context.Blogs,
                    category => category.CategoryID,
                    blog => blog.CategoryID,
                    (category, blogs) => new GetCategoryWithBlogCountResult
                    {
                        CategoryID = category.CategoryID, // DTO sınıfındaki ile aynı olmalı
                        CategoryName = category.Name,
                        BlogCount = blogs.Count()
                    })
                .ToListAsync();

            return result;
        }

    }
}
