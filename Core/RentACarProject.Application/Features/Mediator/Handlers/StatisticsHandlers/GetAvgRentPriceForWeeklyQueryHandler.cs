using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetAvgRentPriceForWeeklyQueryHandler : IRequestHandler<GetAvgRentPriceForWeeklyQuery, GetAveragePriceForWeeklyQueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetAvgRentPriceForWeeklyQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAveragePriceForWeeklyQueryResult> Handle(GetAvgRentPriceForWeeklyQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetAvgRentPriceForWeeklyAsync();
            return new GetAveragePriceForWeeklyQueryResult
            {
                AvgRentPriceForWeekly = value
            };
        }
    }
}
