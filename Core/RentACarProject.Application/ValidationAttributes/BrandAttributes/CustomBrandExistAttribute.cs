using System.ComponentModel.DataAnnotations;
using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;

namespace RentACarProject.Application.ValidationAttributes.BrandAttributes
{
    public class CustomBrandExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var command = (CreateBrandCommand)validationContext.ObjectInstance;

            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
