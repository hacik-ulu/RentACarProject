using RentACarProject.Dto.BlogDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Domain.Entities
{
    public class ResultPagedDto
    {
        public List<ResultAllBlogsWithAuthorDto> Results { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

}
