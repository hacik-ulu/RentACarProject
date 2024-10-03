using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands
{
    public class CreateCarPricingCommand : IRequest
    {
        [Required(ErrorMessage = "CarID is required.")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "At least one PricingID is required.")]
        [MinLength(1, ErrorMessage = "At least one PricingID is required.")]
        public List<int> PricingIDs { get; set; } = new List<int>(); // Birden fazla pricing ID'si

        [Required(ErrorMessage = "At least one amount is required.")]
        [MinLength(3, ErrorMessage = "You must provide amounts for Daily, Weekly, and Monthly.")]
        [MaxLength(3, ErrorMessage = "You can enter a maximum of 3 amounts.")]
        public List<decimal> Amounts { get; set; } = new List<decimal>();
    }
}
