﻿using MediatR;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarDescriptionCommands
{
    public class UpdateCarDescriptionCommand : IRequest
    {
        [Required(ErrorMessage = "CarDescriptionID is required")]
        public int CarDescriptionID { get; set; }

        [Required(ErrorMessage = "Car Model is required")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Details are required")]
        [StringLength(1000, MinimumLength = 100, ErrorMessage = "Details must be between 100 and 1000 characters")]
        public string Details { get; set; }
    }
}
