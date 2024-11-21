using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.CarPricingDtos
{
    public class UpdateCarPricingDto
    {
        [Required(ErrorMessage = "Car ID is required.")]
        public int? CarID { get; set; }

        [Required(ErrorMessage = "At least one pricing amount is required.")]
        [MinLength(1, ErrorMessage = "You must provide at least one pricing amount.")]
        public List<PricingAmountDto?> PricingAmounts { get; set; } = new List<PricingAmountDto?>();
    }

    public class PricingAmountDto
    {
        [Required(ErrorMessage = "Pricing ID is required.")]
        public int? PricingID { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal? Amount { get; set; }
    }
}
