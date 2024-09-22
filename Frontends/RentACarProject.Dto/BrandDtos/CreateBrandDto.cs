using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.BrandDtos
{
    public class CreateBrandDto
    {
        [Required(ErrorMessage = "Brand name is required.")]
        //[MinLength(2, ErrorMessage = "Brand name must be at least 2 characters long.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        public string Name { get; set; }

    }
}
