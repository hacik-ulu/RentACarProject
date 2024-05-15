using MediatR;
using RentACarProject.Application.Features.Mediator.Results.CarPricingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.CarPricingQueries
{
	public class GetCarPricingWithTimePeriodQuery : IRequest<List<GetCarPricingWithTimePeriodQueryResult>>
	{
	}
}
