﻿using RentACarProject.Application.Features.Mediator.Commands.LocationCommands;
using RentACarProject.Application.Features.Mediator.Commands.PricingCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.ValidationAttributes.PricingTypeAttributes.CreatePricingTypeAttributes
{
    public class CustomPricingTypeExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Eğer değer null ise, diğer validasyonların çalışmasını sağlamak için doğrulama yapmıyoruz
            if (value == null)
            {
                return ValidationResult.Success; // Null ise hata mesajı döndürme -- Required mesajı döndürecek null için
            }

            var command = (CreatePricingCommand)validationContext.ObjectInstance;

            if (command.IsExist(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
