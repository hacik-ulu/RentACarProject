using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Queries.CategoryQueries
{
    public class GetCategoryWithBlogCountQuery
    {
        public int CategoryID { get; set; }

        public GetCategoryWithBlogCountQuery(int categoryId)
        {
            CategoryID = categoryId;
        }
    }
}
