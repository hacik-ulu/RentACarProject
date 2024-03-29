using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.TagCloudDtos
{
    public class GetByBlogIdtagCloudDto
    {
        public int TagCloudID { get; set; }
        public string Name { get; set; }
        public int BlogID { get; set; }
    }
}
