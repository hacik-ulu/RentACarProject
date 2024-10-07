using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarDescriptionCommands
{
    public class CreateCarDescriptionCommand : IRequest
    {
        [Required(ErrorMessage = "CarID is required")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Details are required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Details must be between 10 and 500 characters")]
        public string Details { get; set; }
    }

}
