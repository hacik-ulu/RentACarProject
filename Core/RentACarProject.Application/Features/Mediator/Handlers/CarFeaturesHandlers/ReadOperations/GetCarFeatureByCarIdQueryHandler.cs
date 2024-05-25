using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarFeatureQueries;
using RentACarProject.Application.Features.Mediator.Results.BlogResults;
using RentACarProject.Application.Features.Mediator.Results.CarFeaturesResults;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarFeaturesHandlers.ReadOperations
{
    public class GetCarFeatureByCarIdQueryHandler : IRequestHandler<GetCarFeatureByCarIdQuery, List<GetCarFeatureByCarIdQueryResult>>
    {
        private readonly ICarFeatureRepository _repository;

        public GetCarFeatureByCarIdQueryHandler(ICarFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCarFeatureByCarIdQueryResult>> Handle(GetCarFeatureByCarIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetCarFeaturesByCarIDAsync(request.Id);
            return values.Select(x => new GetCarFeatureByCarIdQueryResult
            {
                Availability = x.Availability,
                CarFeatureID = x.CarFeatureID,
                FeatureName = x.Feature.Name,
                FeatureID = x.FeatureID
            }).ToList();

        }
    }
}
