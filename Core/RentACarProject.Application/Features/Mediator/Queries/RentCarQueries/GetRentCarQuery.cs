using MediatR;
using RentACarProject.Application.Features.Mediator.Results.RentCarResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.RentCarQueries
{
    public class GetRentCarQuery : IRequest<List<GetRentCarQueryResult>>
    {
        // CarID getirilecek ama LocationID ve Available koşuluna göre
        public int LocationID { get; set; }
        public bool Available { get; set; }
    }
}
