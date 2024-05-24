using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Results.CategoryResults
{
    public class GetCategoryWithBlogCountResult
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int BlogCount { get; set; }
    }
}
