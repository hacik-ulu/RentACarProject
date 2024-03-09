using MediatR;
using RentACarProject.Application.Features.Mediator.Results.TestimonialResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.TestimonailsQueries
{
    public class GetTestimonialQuery : IRequest<List<GetTestimonialQueryResult>>
    {
    }
}
