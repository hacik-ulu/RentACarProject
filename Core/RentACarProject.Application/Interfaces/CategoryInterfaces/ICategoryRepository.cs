
using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryRepository
    {
        Task<GetCategoryWithBlogCountResult> GetCategoryBlogCountAsync(int categoryId);

    }
}
