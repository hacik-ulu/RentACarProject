using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands
{
    public class UpdateCarPricingCommand : IRequest
    {
        [Required(ErrorMessage = "CarID is required.")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "At least one pricing amount is required.")]
        public List<PricingUpdateDto> PricingAmounts { get; set; } = new List<PricingUpdateDto>();

        public class PricingUpdateDto
        {
            [Required(ErrorMessage = "PricingID is required.")]
            public int PricingID { get; set; }

            [Required(ErrorMessage = "Amount is required.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
            public decimal Amount { get; set; }
        }
    }
}
