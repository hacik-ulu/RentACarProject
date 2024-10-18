using MediatR;
using RentACarProject.Application.Features.Mediator.Results.ReservationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.ReservationQueries
{
    public class GetReservationByUserIdQuery : IRequest<List<GetReservationByUserIdQueryResult>>
    {
        public int Id { get; set; }
        public GetReservationByUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
