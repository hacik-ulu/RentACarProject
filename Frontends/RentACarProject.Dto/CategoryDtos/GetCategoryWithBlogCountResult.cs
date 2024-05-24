using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CategoryDtos
{
    public class GetCategoryWithBlogCountResult
    {

        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public int blogCount { get; set; }

    }
}
