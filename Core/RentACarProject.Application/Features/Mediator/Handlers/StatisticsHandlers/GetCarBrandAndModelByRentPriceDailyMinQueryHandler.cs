using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarBrandAndModelByRentPriceDailyMinQueryHandler : IRequestHandler<GetCarBrandAndModelByRentPriceDailyMinQuery, GetCarBrandNameAndModelByRentPriceDailyMinQueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetCarBrandAndModelByRentPriceDailyMinQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarBrandNameAndModelByRentPriceDailyMinQueryResult> Handle(GetCarBrandAndModelByRentPriceDailyMinQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetCarBrandAndModelByRentPriceDailyMinAsync();
            return new GetCarBrandNameAndModelByRentPriceDailyMinQueryResult
            {
                CarBrandNameAndModelByRentPriceDailyMin = value
            };
        }
    }
}
