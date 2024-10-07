using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.BlogCommands;
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
    public class CreateCarDescriptionHandler : IRequestHandler<CreateCarDescriptionCommand>
    {
        private readonly IRepository<CarDescription> _repository;
        public CreateCarDescriptionHandler(IRepository<CarDescription> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCarDescriptionCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new CarDescription
            {
                Details = request.Details,
                CarID = request.CarID
            });
        }
    }
}
