using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands
{
    public class UpdateCarPricingCommand :IRequest
    {
        [Required(ErrorMessage = "CarPricingID is required.")]
        public int CarPricingID { get; set; }

        [Required(ErrorMessage = "CarID is required.")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "PricingID is required.")]
        public int PricingID { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, 50000, ErrorMessage = "Amount must be between 0 and 50,000.")]
        public decimal Amount { get; set; }
    }
}
