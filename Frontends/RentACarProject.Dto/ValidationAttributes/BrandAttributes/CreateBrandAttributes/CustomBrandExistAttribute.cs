using RentACarProject.Dto.BrandDtos;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Dto.ValidationAttributes.BrandAttributes.CreateBrandAttributes
{
    public class CustomBrandExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Eğer değer null ise, diğer validasyonların çalışmasını sağlamak için doğrulama yapmıyoruz
            if (value == null)
            {
                return ValidationResult.Success; // Null ise hata mesajı döndürme (Required attribute dönecek)
            }

            var command = (CreateBrandDto)validationContext.ObjectInstance;

            // Eğer marka zaten var ise, hata mesajı döndür
            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
