using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarDescriptionDtos
{
    public class CreateCarDescriptionDto
    {
        [Required(ErrorMessage = "Car Model is required")]
        public int? CarID { get; set; }

        [Required(ErrorMessage = "Details are required")]
        [StringLength(1000, MinimumLength = 100, ErrorMessage = "Details must be between 100 and 1000 characters")]
        public string? Details { get; set; }
    }
}
