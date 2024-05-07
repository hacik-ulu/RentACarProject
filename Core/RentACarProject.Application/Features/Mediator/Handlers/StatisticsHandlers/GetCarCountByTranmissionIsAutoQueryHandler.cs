using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.StatisticsQueries;
using RentACarProject.Application.Features.Mediator.Results.StatisticsResult;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarCountByTranmissionIsAutoQueryHandler : IRequestHandler<GetCarCountByTranmissionIsAutoQuery, GetCarCountByTransmissionIsAutoQueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetCarCountByTranmissionIsAutoQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarCountByTransmissionIsAutoQueryResult> Handle(GetCarCountByTranmissionIsAutoQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetCarCountByTranmissionIsAutoAsync();
            return new GetCarCountByTransmissionIsAutoQueryResult
            {
                CarCountByTransmissionIsAuto = value
            };
        }
    }
}
