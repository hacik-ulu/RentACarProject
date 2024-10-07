using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarDescriptionCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarDescriptionHandlers.WriteOperations
{
    public class UpdateCarDescriptionHandler : IRequestHandler<UpdateCarDescriptionCommand>
    {
        private readonly IRepository<CarDescription> _repository;
        public UpdateCarDescriptionHandler(IRepository<CarDescription> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateCarDescriptionCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.CarDescriptionID);
            value.CarID = request.CarID;
            value.Details = request.Details;
            await _repository.UpdateAsync(value);
        }
    }
}
