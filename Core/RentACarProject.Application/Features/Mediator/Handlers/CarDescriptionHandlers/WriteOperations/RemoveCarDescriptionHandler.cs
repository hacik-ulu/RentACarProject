using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarDescriptionCommands;
using RentACarProject.Application.Features.Mediator.Commands.FeatureCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarDescriptionHandlers.WriteOperations
{
    public class RemoveCarDescriptionHandler : IRequestHandler<RemoveCarDescriptionCommand>
    {
        private readonly IRepository<CarDescription> _repository;
        public RemoveCarDescriptionHandler(IRepository<CarDescription> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveCarDescriptionCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
