using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Features.Mediator.Commands.FeatureCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.WriteOperations
{
    public class CreateCarPricingHandler : IRequestHandler<CreateCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;
        public CreateCarPricingHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateCarPricingCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new CarPricing
            {
                CarID = request.CarID,
                PricingID = request.PricingID,
                Amount = request.Amount
            });
        }
    }
}
