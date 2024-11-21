using MediatR;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands
{
    public class CreateCarFeatureByCarCommand : IRequest
    {
        public int CarID { get; set; }
        //public int FeatureID { get; set; }
        public List<int> FeatureIDs { get; set; }
        //public bool Availability { get; set; }
    }
}

// Get ederken(create feature icin) available ==1 ise checkboxa ya da kontrole gelmesin.