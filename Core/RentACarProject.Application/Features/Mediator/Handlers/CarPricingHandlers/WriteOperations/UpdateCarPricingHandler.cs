using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.BlogCommands;
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
    public class UpdateCarPricingHandler : IRequestHandler<UpdateCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;

        public UpdateCarPricingHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateCarPricingCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.CarPricingID);
            values.Amount = request.Amount;
            values.CarID = request.CarID;
            values.PricingID = request.PricingID;
            await _repository.UpdateAsync(values);
        }
    }
}
