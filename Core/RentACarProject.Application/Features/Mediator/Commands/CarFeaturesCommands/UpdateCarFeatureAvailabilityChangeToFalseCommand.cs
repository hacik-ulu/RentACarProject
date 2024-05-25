using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands
{
    public class UpdateCarFeatureAvailabilityChangeToFalseCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCarFeatureAvailabilityChangeToFalseCommand(int id)
        {
            Id = id;
        }

    }
}
