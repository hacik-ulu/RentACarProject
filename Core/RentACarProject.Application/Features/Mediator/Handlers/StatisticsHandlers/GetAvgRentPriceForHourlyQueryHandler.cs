using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetAvgRentPriceForHourlyQueryHandler : IRequestHandler<GetAvgRentPriceForHourlyQuery, GetAvgRentPriceForHourlyQueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetAvgRentPriceForHourlyQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAvgRentPriceForHourlyQueryResult> Handle(GetAvgRentPriceForHourlyQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetAvgRentPriceForHourlyAsync();
            return new GetAvgRentPriceForHourlyQueryResult
            {
                AvgRentPriceForHourly = value
            };
        }
    }
}
