using RentACarProject.Dto.BrandDtos;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.WebUI.ValidationAttributes.BrandAttributes
{
    public class CustomBrandExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var command = (CreateBrandDto)validationContext.ObjectInstance;

            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
