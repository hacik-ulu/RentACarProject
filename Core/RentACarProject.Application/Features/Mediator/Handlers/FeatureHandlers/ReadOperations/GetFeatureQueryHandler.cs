using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.FeatureQueries;
using RentACarProject.Application.Features.Mediator.Results.FeatureResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.FeatureHandlers.ReadOperations
{
    public class GetFeatureQueryHandler : IRequestHandler<GetFeatureQuery, List<GetFeatureQueryResult>>
    {
        private readonly IRepository<Feature> _repository;

        public GetFeatureQueryHandler(IRepository<Feature> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFeatureQueryResult>> Handle(GetFeatureQuery request, CancellationToken cancellationToken)
        {
            // values'den aldığımız değerleri x ile gezip yeni bir GetFeatureQueryResult oluşturuyoruz.
            // buradaki xleri yani values nesnelerini GetFeatureQueryResult Tipine dönüştürüyoruz.

            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetFeatureQueryResult
            {
                FeatureID = x.FeatureID,
                Name = x.Name,
            }).ToList();
        }
    }
}
