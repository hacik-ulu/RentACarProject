using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands
{
    public class UpdateCarFeatureAvailabilityChangeToTrueCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCarFeatureAvailabilityChangeToTrueCommand(int id)
        {
            Id = id;
        }

    }
}
