using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.CarPricingDtos
{
    public class CreateCarPricingDto
    {
        [Required(ErrorMessage = "Car Model is required.")]
        public int? CarID { get; set; }

        [Required(ErrorMessage = "At least one pricing ID is required.")]
        public List<int?> PricingIDs { get; set; } = new List<int?>();  // Burayı güncelledik.

        [Required(ErrorMessage = "At least one amount is required.")]
        [MinLength(3, ErrorMessage = "You must provide amounts for Daily, Weekly, and Monthly.")]
        [MaxLength(3, ErrorMessage = "You can enter a maximum of 3 amounts.")]
        public List<decimal?> Amounts { get; set; } = new List<decimal?>();
    }
}
