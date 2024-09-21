using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Results.AuthorResults
{
    public class GetBlogsByAuthorIdQueryResult
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
    }
}
