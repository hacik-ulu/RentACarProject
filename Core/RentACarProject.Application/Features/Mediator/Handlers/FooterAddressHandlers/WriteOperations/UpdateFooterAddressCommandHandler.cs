﻿using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.FooterAddressCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.FooterAddressHandlers.WriteOperations
{
    public class UpdateFooterAddressCommandHandler : IRequestHandler<UpdateFooterAddressCommand>
    {
        private readonly IRepository<FooterAddress> _repository;

        public UpdateFooterAddressCommandHandler(IRepository<FooterAddress> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFooterAddressCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.FooterAddressID);
            value.Phone = request.Phone;
            value.Address = request.Address;
            value.Description = request.Description;
            value.Email = request.Email;
            await _repository.UpdateAsync(value);
        }
    }
}
