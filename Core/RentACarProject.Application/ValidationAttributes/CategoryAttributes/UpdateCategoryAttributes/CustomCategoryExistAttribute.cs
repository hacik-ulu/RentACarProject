﻿using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.ValidationAttributes.CategoryAttributes.UpdateCategoryAttributes
{
    public class CustomCategoryExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Eğer değer null ise, diğer validasyonların çalışmasını sağlamak için doğrulama yapmıyoruz
            if (value == null)
            {
                return ValidationResult.Success; // Null ise hata mesajı döndürme (Required attribute dönecek)
            }

            var command = (UpdateCategoryCommand)validationContext.ObjectInstance;

            // Eğer marka zaten var ise, hata mesajı döndür
            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}