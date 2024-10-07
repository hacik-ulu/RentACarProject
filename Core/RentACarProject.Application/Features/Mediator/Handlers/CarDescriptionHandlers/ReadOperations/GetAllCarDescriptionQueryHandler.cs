using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarDescriptionQueries;
using RentACarProject.Application.Features.Mediator.Results.CarDescriptionResults;
using RentACarProject.Application.Interfaces.CarDescriptionInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarDescriptionHandlers.ReadOperations
{
    public class GetAllCarDescriptionQueryHandler : IRequestHandler<GetAllCarDescriptionsQuery, List<GetCarDescriptionQueryResult>>
    {
        private readonly ICarDescriptionRepository _repository;
        public GetAllCarDescriptionQueryHandler(ICarDescriptionRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCarDescriptionQueryResult>> Handle(GetAllCarDescriptionsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllCarDescriptionAsync();
            return values.Select(x => new GetCarDescriptionQueryResult
            {
                CarDescriptionID = x.CarDescriptionID,
                CarID = x.CarID,
                Details = x.Details
            }).ToList(); // Liste olarak döndürmek için ToList() eklenir
        }
    }
}
