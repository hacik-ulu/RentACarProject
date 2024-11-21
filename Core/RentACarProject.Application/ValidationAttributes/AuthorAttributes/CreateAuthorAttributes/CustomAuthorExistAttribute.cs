using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;
using RentACarProject.Application.Features.Mediator.Commands.AuthorCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.ValidationAttributes.AuthorAttributes.CreateAuthorAttributes
{
    public class CustomAuthorExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Eğer değer null ise, diğer validasyonların çalışmasını sağlamak için doğrulama yapmıyoruz
            if (value == null)
            {
                return ValidationResult.Success; // Null ise hata mesajı döndürme -- Required mesajı döndürecek null için
            }

            var command = (CreateAuthorCommand)validationContext.ObjectInstance;

            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
