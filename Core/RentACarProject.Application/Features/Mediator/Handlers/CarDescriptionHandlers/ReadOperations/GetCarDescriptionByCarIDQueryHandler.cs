using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarDescriptionQueries;
using RentACarProject.Application.Features.Mediator.Results.CarDescriptionResults;
using RentACarProject.Application.Interfaces.CarDescriptionInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.CarDescriptionHandlers
{
    public class GetCarDescriptionByCarIDQueryHandler : IRequestHandler<GetCarDescriptionByCarIDQuery, GetCarDescriptionQueryResult>
    {
        private readonly ICarDescriptionRepository _repository;
        public GetCarDescriptionByCarIDQueryHandler(ICarDescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarDescriptionQueryResult> Handle(GetCarDescriptionByCarIDQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetCarDescriptionAsync(request.Id);
            return new GetCarDescriptionQueryResult
            {
                CarDescriptionID = values.CarDescriptionID,
                CarID = values.CarID,
                Details = values.Details
            };
        }
    }
}