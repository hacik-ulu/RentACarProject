using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.CarPricingDtos
{
    public class CreateCarPricingDto : IValidatableObject
    {
        [Required(ErrorMessage = "Car Model is required.")]
        public int? CarID { get; set; }

        [Required(ErrorMessage = "At least one pricing ID is required.")]
        public List<int?> PricingIDs { get; set; } = new List<int?>();

        public List<decimal?> Amounts { get; set; } = new List<decimal?>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amounts == null || Amounts.Count != 3)
            {
                yield return new ValidationResult("You must provide exactly three amounts for Daily, Weekly, and Monthly.", new[] { nameof(Amounts) });
            }

            if (Amounts != null)
            {
                if (Amounts[0] == null)
                {
                    yield return new ValidationResult("Daily amount is required.", new[] { "Amounts[0]" });
                }
                if (Amounts[1] == null)
                {
                    yield return new ValidationResult("Weekly amount is required.", new[] { "Amounts[1]" });
                }
                if (Amounts[2] == null)
                {
                    yield return new ValidationResult("Monthly amount is required.", new[] { "Amounts[2]" });
                }
            }
        }
    }
}
