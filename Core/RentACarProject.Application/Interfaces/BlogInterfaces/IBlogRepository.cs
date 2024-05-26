using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.BlogInterfaces
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetLastThreeBlogsWithAuthorsAsync();
        Task<List<Blog>> GetLastEightBlogsAsync();
        Task<List<Blog>> GetAllBlogsWithAuthorsAsync();
        Task<List<Blog>> GetBlogByAuhorIdAsync(int id);
        Task<string> GetCategoryNameByIdAsync(int categoryID);
    }
}
