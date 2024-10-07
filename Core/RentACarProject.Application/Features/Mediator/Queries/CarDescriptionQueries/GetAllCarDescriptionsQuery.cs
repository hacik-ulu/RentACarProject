using MediatR;
using RentACarProject.Application.Features.Mediator.Results.CarDescriptionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.CarDescriptionQueries
{
    public class GetAllCarDescriptionsQuery : IRequest<List<GetCarDescriptionQueryResult>>
    {
    }
}
