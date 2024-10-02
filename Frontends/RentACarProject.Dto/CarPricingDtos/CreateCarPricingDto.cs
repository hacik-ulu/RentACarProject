using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarPricingDtos
{
    public class CreateCarPricingDto
    {
        [Required(ErrorMessage = ("Car Model is required."))]
        public int? CarID { get; set; }

        [Required(ErrorMessage = ("Pricing Type is required."))]
        public int? PricingID { get; set; }

        [Required(ErrorMessage = ("Amount is required."))]
        [Range(0, 50000, ErrorMessage = "Amount must be between 0 and 50,000.")]
        public decimal? Amount { get; set; }
    }
}
