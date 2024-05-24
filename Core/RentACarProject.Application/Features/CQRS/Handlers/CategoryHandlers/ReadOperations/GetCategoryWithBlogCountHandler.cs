using RentACarProject.Application.Features.CQRS.Queries.CategoryQueries;
using RentACarProject.Application.Features.CQRS.Results.CategoryResults;
using RentACarProject.Application.Interfaces.CategoryInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers.ReadOperations
{
    public class GetCategoryWithBlogCountHandler
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryWithBlogCountHandler(ICategoryRepository categoryInterface)
        {
            _repository = categoryInterface;
        }

        public async Task<GetCategoryWithBlogCountResult> Handle(GetCategoryWithBlogCountQuery query)
        {
            var values = await _repository.GetCategoryBlogCountAsync(query.CategoryID);
            return new GetCategoryWithBlogCountResult
            {
                CategoryID = values.CategoryID,
                BlogCount = values.BlogCount,
                CategoryName = values.CategoryName
            };
        }
    }
}
