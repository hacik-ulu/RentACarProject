using MediatR;
using RentACarProject.Application.Features.Mediator.Results.PricingResults;
using RentACarProject.Application.Features.Mediator.Results.ReservatioResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.ReservationQueries
{
    public class GetReservationQuery : IRequest<List<GetReservationQueryResult>>
    {
    }
}
