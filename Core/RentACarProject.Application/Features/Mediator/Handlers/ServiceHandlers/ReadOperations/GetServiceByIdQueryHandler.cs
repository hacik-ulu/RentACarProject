﻿using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.ServiceQueries;
using RentACarProject.Application.Features.Mediator.Results.ServiceHandlers;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class GetPricingByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdQueryResult>
    {
        private readonly IRepository<Service> _repository;
        public GetPricingByIdQueryHandler(IRepository<Service> repository)
        {
            _repository = repository;
        }

        public async Task<GetServiceByIdQueryResult> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetServiceByIdQueryResult
            {
                ServiceId = values.ServiceId,
                Description = values.Description,
                Title = values.Title,
                ImageUrl = values.ImageUrl
            };
        }
    }
}