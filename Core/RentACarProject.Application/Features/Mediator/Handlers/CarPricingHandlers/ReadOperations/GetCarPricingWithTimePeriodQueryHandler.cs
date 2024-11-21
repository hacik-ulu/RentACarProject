using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarPricingQueries;
using RentACarProject.Application.Features.Mediator.Results.CarPricingResults;
using RentACarProject.Application.Interfaces.CarPricingInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.ReadOperations
{
	public class GetCarPricingWithTimePeriodQueryHandler : IRequestHandler<GetCarPricingWithTimePeriodQuery, List<GetCarPricingWithTimePeriodQueryResult>>
	{
		private readonly ICarPricingRepository _repository;
		private readonly IRepository<Car> _carRepository;
        public GetCarPricingWithTimePeriodQueryHandler(ICarPricingRepository repository, IRepository<Car> carRepository)
        {
            _repository = repository;
            _carRepository = carRepository;
        }
        public async Task<List<GetCarPricingWithTimePeriodQueryResult>> Handle(GetCarPricingWithTimePeriodQuery request, CancellationToken cancellationToken)
		{
			var values = await _repository.GetCarPricingWithTimePeriodAsync();
			return values.Select(x => new GetCarPricingWithTimePeriodQueryResult
			{
				CarID = x.CarID,
				Brand = x.Brand,
				Model = x.Model,
				CoverImagerUrl = x.CoverImagerUrl,
				DailyAmount = x.Amounts[0],
				WeeklyAmount = x.Amounts[1],
				MonthlyAmount = x.Amounts[2]
			}).ToList();
		}
	}
}
