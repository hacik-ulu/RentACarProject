using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarDescriptionQueries;
using RentACarProject.Application.Features.Mediator.Results.CarDescriptionResults;
using RentACarProject.Application.Interfaces.CarDescriptionInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.CarDescriptionHandlers
{
    public class GetCarDescriptionByCarIDQueryHandler : IRequestHandler<GetCarDescriptionByCarIDQuery, GetCarDescriptionQueryResult>
    {
        private readonly IRepository<CarDescription> _repository;
        public GetCarDescriptionByCarIDQueryHandler(IRepository<CarDescription> repository)
        {
            _repository = repository;
        }

        public async Task<GetCarDescriptionQueryResult> Handle(GetCarDescriptionByCarIDQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetCarDescriptionQueryResult
            {
                CarDescriptionID = values.CarDescriptionID,
                CarID = values.CarID,
                Details = values.Details
            };
        }
    }
}