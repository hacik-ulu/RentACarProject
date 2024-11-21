using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;
using RentACarProject.Dto.BrandDtos;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.ValidationAttributes.BrandAttributes.CreateBrandAttributes
{
    public class CustomBrandExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Eğer değer null ise, diğer validasyonların çalışmasını sağlamak için doğrulama yapmıyoruz
            if (value == null)
            {
                return ValidationResult.Success; // Null ise hata mesajı döndürme -- Required mesajı döndürecek null için
            }

            var command = (CreateBrandCommand)validationContext.ObjectInstance;

            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
