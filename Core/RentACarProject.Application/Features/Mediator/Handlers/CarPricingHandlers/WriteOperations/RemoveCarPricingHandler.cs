using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.WriteOperations
{
    public class RemoveCarPricingCommandHandler : IRequestHandler<RemoveCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;

        public RemoveCarPricingCommandHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCarPricingCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(values);
        }
    }
}
