using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarCountByKmSmallerThen1000QueryHandler : IRequestHandler<GetCarCountByKmSmallerThen1000Query, GetCarCountByKmSmallerThen1000QueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetCarCountByKmSmallerThen1000QueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarCountByKmSmallerThen1000QueryResult> Handle(GetCarCountByKmSmallerThen1000Query request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetCarCountByKmSmallerThen1000Async();
            return new GetCarCountByKmSmallerThen1000QueryResult
            {
                CarCountByKmSmallerThen1000 = value
            };
        }
    }
}
