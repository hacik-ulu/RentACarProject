using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarFeaturesCommands;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarFeaturesHandlers.ReadOperations
{
    public class UpdateCarFeatureAvailabilityChangeToFalseCommandHandler : IRequestHandler<UpdateCarFeatureAvailabilityChangeToFalseCommand>
    {
        private readonly ICarFeatureRepository _repository;

        public UpdateCarFeatureAvailabilityChangeToFalseCommandHandler(ICarFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCarFeatureAvailabilityChangeToFalseCommand request, CancellationToken cancellationToken)
        {
            _repository.ChangeCarFeatureAvailabilityToFalse(request.Id);
        }
    }
}
