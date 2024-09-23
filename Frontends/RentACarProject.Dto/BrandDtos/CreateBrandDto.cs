using Microsoft.AspNetCore.Mvc;
using ServiceStack.DataAnnotations;
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
        [BindProperty]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Brand name is required.")]
        [System.ComponentModel.DataAnnotations.StringLength(25, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 25 characters long.")]
        [Unique]
        public string Name { get; set; }

    }
}
