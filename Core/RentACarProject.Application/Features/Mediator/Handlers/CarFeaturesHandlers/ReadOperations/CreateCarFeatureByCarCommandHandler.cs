using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarFeaturesHandlers.ReadOperations
{
    public class CreateCarFeatureByCarCommandHandler : IRequestHandler<CreateCarFeatureByCarCommand>
    {
        private readonly ICarFeatureRepository _repository;

        public CreateCarFeatureByCarCommandHandler(ICarFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCarFeatureByCarCommand request, CancellationToken cancellationToken)
        {
            foreach (var featureId in request.FeatureIDs)
            {
                _repository.CreateCarFeatureByCar(new CarFeature
                {
                    Availability = true,
                    CarID = request.CarID,
                    FeatureID = featureId,
                });
            }
        }


        //public async Task Handle(CreateCarFeatureByCarCommand request, CancellationToken cancellationToken)
        //{
        //    _repository.CreateCarFeatureByCar(new CarFeature
        //    {
        //        Availability = true,
        //        CarID = request.CarID,
        //        //FeatureID = request.FeatureID,               
        //    });

        //}
    }
}
